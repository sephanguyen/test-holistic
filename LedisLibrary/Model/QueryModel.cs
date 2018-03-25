
using System.Collections.Generic;

namespace LedisLibrary.Model
{
    public class QueryModel
    {
        public string Command { get; set; }
        public string Key { get; set; }
        public IEnumerable<string> Values{get;set;}
    }
}
