using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedisLibrary.LedisCollection
{
    public interface ILedisCollection
    {
        IEnumerable<string> KEYS(string expression);
        int DEL(string key);
        string FLUSHDB();
        int EXPIRE(string key, int seconds);
        int TTL(string key);
        object LLEN(string key);
        object RPUSH(string key, List<string> values);
        string LPOP(string key);
        string RPOP(string key);
        IEnumerable<string> LRANGE(string key, int start, int stop);
        object SADD(string key, HashSet<string> values);
        object SCARD(string key);
        IEnumerable<string> SMEMBERS(string key);
        object SREM(string key, HashSet<string> values);
        IEnumerable<string> SINTER(List<string> keys);
        string SET(string key, string value);
        string GET(string key);
    }
}
