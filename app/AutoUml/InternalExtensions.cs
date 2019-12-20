using System.Collections.Generic;
using System.Linq;

namespace AutoUml
{
    internal static class InternalExtensions
    {
        public static IEnumerable<string> SplitLines(this string text, bool cutEmpty)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            var lines = text
                .Replace("\r\n", "\n")
                .Split('\n');
            if (cutEmpty)
                lines = lines
                    .Where(a => !string.IsNullOrEmpty(a?.Trim()))
                    .ToArray();
            return lines.Length == 0 ? null : lines;
        }
        
        public static IEnumerable<string> SplitLines(this PlantUmlText ptext, bool cutEmpty)
        {
            return ptext?.Text.SplitLines(cutEmpty);
        }

        public static HashSet<Type> ToHashSet<Type>(this IEnumerable<Type> src)
        {
            var result = new HashSet<Type>();
            foreach (var i in src)
                result.Add(i);
            return result;
        }
    }
}