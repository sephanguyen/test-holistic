using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace LedisLibrary.LedisCollection
{
    [Serializable]
    public class LedisCollectionInstance : ILedisCollection 
    {
        protected Dictionary<string, object> Collection;
        private Dictionary<string, Tuple<TimeDelay, Timer>> _dictionaryExpire = new Dictionary<string, Tuple<TimeDelay, Timer>>();

        public LedisCollectionInstance()
        {
            Collection = new Dictionary<string, object>();
        }
        public int DEL(string key)
        {
            if (Collection.ContainsKey(key))
            {
                var indexKey = Array.IndexOf(Collection.Keys.ToArray(), key);
                Collection.Remove(key);
                return indexKey;
            }
            return -1;
        }

        public int EXPIRE(string key, int seconds)
        {
            if (Collection.ContainsKey(key))
            {
                var oldSenconds = TTL(key);
                if(oldSenconds == 0 || oldSenconds > seconds)
                {
                    var time = new TimeDelay(seconds);
                    var timerExpire = new Timer();
                    timerExpire.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) => {
                        if (time.Seconds == 0)
                        {
                            Task.Run(() =>
                            {
                                Collection.Remove(key);
                                _dictionaryExpire.Remove(key);
                                timerExpire.Stop();
                                timerExpire.Dispose();
                            });
                        }
                        --time.Seconds;
                    });
                    timerExpire.Interval = 1000;
                    timerExpire.Enabled = true;
                    Tuple<TimeDelay, Timer> tupeExpire = new Tuple<TimeDelay, Timer>(time, timerExpire);

                    if (_dictionaryExpire.ContainsKey(key))
                    {
                        _dictionaryExpire.Remove(key);
                    }
                    _dictionaryExpire.Add(key, tupeExpire);
                }
                return seconds;
            }
            return 0;
        }

        public string FLUSHDB()
        {
            Collection.Clear();
            return "OK";
        }

        public IEnumerable<string> KEYS(string pattern = "*" )
        {
            var keyCollection = Collection.Keys.ToList();
            if(pattern == "*")
            {
                return keyCollection;
            }
            var myRegex = new Regex(@pattern);
            return keyCollection.Where(f => myRegex.IsMatch(f)).ToList();
        }

        public int TTL(string key)
        {
            if (_dictionaryExpire.ContainsKey(key))
            {
                var tupeExpire = _dictionaryExpire[key];
                return tupeExpire.Item1.Seconds ;
            }
            return 0;
        }

    #region Ledish List
        public object LLEN(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is List<string>)
                {
                    IList<string> listLedis = Collection[key] as List<string>;
                    return listLedis.Count;
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return 0;
        }

        public object RPUSH(string key, List<string> values)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is List<string>)
                {
                    var lisLedis = Collection[key] as List<string>;
                    lisLedis.AddRange(values);
                    return lisLedis.Count;
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }

            Collection[key] = values;
            return values.Count;
        }

        public string LPOP(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if(Collection[key] is List<string>)
                {
                    var lisLedis = Collection[key] as List<string>;
                    var count = lisLedis.Count;
                    if (count > 0)
                    {
                        var result = lisLedis[0];
                        lisLedis.RemoveAt(0);
                        return result;
                    }
                    return "ERROR: List empty";
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return "ERROR: Key not found";
        }
        public string RPOP(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is List<string>)
                {
                    var lisLedis = Collection[key] as List<string>;
                    var count = lisLedis.Count;
                    if (count > 0)
                    {
                        var lastIndex = count - 1;
                        var result = lisLedis[lastIndex];
                        lisLedis.RemoveAt(lastIndex);
                        return result;
                    }
                    return "ERROR: List empty";
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return "ERROR: Key not found";
        }

        public IEnumerable<string> LRANGE(string key, int start, int stop)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is List<string>)
                {
                    if (stop > start)
                    {
                        var lisLedis = Collection[key] as List<string>;
                        var count = lisLedis.Count;
                        if (count > 0)
                        {
                            var lastIndex = count - 1;
                            if (stop > lastIndex)
                            {
                                stop = lastIndex;
                            }
                            var result = lisLedis.GetRange(start, (stop - start) + 1);
                            return result;
                        }
                        return new[] { "ERROR: List empty" };
                    }
                    return new[] { string.Empty };
                }
                return new[] { "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value" };
            }
            return new[] { "ERROR: Key not found" };
        }
        #endregion

        #region Set Collection
        public object SADD(string key, HashSet<string> values)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is HashSet<string>)
                {
                    var setLedis = Collection[key] as HashSet<string>;
                    setLedis.UnionWith(values);
                    return setLedis.Count;
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            Collection[key] = values;
            return values.Count;
        }

        public object SCARD(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is HashSet<string>)
                {
                    var setLedis = Collection[key] as HashSet<string>;
                    return setLedis.Count;
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return 0;
        }



        public IEnumerable<string> SMEMBERS(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is HashSet<string>)
                {
                    var setLedis = Collection[key] as HashSet<string>;
                    var count = setLedis.Count;
                    if (count > 0)
                    {
                        String[] member = new String[count];
                        setLedis.CopyTo(member);
                        return member;

                    }
                    return new[] { "ERROR: List empty" };
                }
                return new[] { "ERROR: List empty" };
            }
            return new[] { "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value" };
        }

        public object SREM(string key, HashSet<string> values)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is HashSet<string>)
                {
                    var setLedis = Collection[key] as HashSet<string>;
                    var count = setLedis.Count;
                    if (count > 0)
                    {
                        int countValueDelete = 0;
                        foreach (var value in values)
                        {
                            if (setLedis.Contains(value))
                            {
                                setLedis.Remove(value);
                                countValueDelete++;
                            }

                        }
                        return countValueDelete;
                    }
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return "ERROR: Key not found";
        }

        public IEnumerable<string> SINTER(List<string> keys)
        {
            HashSet<string> intersectionMembers = new HashSet<string>();
            if (Collection.ContainsKey(keys[0]))
            {
                if (!(Collection[keys[0]] is HashSet<string>))
                {
                    return new string[] { "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value" };
                }
                intersectionMembers = new HashSet<string>(Collection[keys[0]] as HashSet<string>);
            }
            for (int i = 1; i < keys.Count; i++)
            {
                if (Collection.ContainsKey(keys[i]))
                {
                    if (!(Collection[keys[0]] is HashSet<string>))
                    {
                        return new string[] { "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value" };
                    }
                    var currentSetOfKey = new HashSet<string>(Collection[keys[i]] as HashSet<string>);
                    intersectionMembers.IntersectWith(currentSetOfKey);
                }
                else
                {
                    return new string[] { };
                }
            }
            return intersectionMembers;
        }
        #endregion

        #region stringLedis
        public string SET(string key, string value)
        {
            if (Collection.ContainsKey(key))
            {
                Collection[key] = value;
            }
            else
            {
                Collection.Add(key, value);
            }
            return "OK";
        }
        public string GET(string key)
        {
            if (Collection.ContainsKey(key))
            {
                if (Collection[key] is string)
                {
                    return Collection[key] as string;
                }
                return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
            }
            return "ERROR: Key not found";
        }
        #endregion
    }

}
