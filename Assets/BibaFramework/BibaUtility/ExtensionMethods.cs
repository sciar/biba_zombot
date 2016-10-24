using System.Collections.Generic;
using System.Linq;

namespace BibaFramework.Utility
{
    public static class ExtensionMethods
    {
        public static string ToDebugString<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray()) + "}";
        }
    }
}