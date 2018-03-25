using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LedisLibrary.Helpers
{
    public static class ConverterParam
    {
        public static List<string> ConvertParamToList(this string param)
        {
            var paramsAfterSplit = param.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return paramsAfterSplit.ToList();
        }

        public static HashSet<string> ConvertParamToHashSet(this string param)
        {
            var paramsAfterSplit = param.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(paramsAfterSplit);
        }
    }
}
