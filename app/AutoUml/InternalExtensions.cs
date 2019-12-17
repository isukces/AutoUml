using System.Collections.Generic;

namespace AutoUml
{
    internal static class InternalExtensions
    {
        public static HashSet<Type> ToHashSet<Type>(this IEnumerable<Type> src)
        {
            var result = new HashSet<Type>();
            foreach (var i in src)
                result.Add(i);
            return result;
        }
    }
}