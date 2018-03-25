using LedisLibrary.LedisCollection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LedisLibrary.LedisStore
{
    [Serializable]
    public class DataLedisStore
    {
        private static DataLedisStore _instance = new DataLedisStore();
        public ILedisCollection LedisCollection { get; private set; }

        private const string filePath = "ledish.dmb";
        internal dynamic RestoreDB()
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                _instance = (DataLedisStore)binaryFormatter.Deserialize(stream);
            }
            return true;
        }

        internal dynamic SaveDB()
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, _instance);
            }
            return true;
        }

        private DataLedisStore()
        {
            LedisCollection = new LedisCollectionInstance();

        }

        public static DataLedisStore GetDataStore()
        {
            return _instance;
        }

        public List<string> GetKeys(string pattern)
        {
            var keys = LedisCollection.KEYS(pattern).ToList();
            return keys;
        }

        public int DelKey(string key)
        {
            var keyDelete = LedisCollection.DEL(key);
            return keyDelete;
        }

        public bool FlushDB()
        {
            LedisCollection.FLUSHDB();
            return true;
        }

        public int Expire(string key, int second)
        {
            var secondExpire = LedisCollection.EXPIRE(key, second);
            return secondExpire;
        }

        public dynamic GetTtl(string key)
        {
            var secondExpire = LedisCollection.TTL(key);
            return secondExpire;
        }

    }

}
