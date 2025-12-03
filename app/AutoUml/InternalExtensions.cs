using System.Collections.Generic;
using System.Linq;

namespace AutoUml;

internal static class InternalExtensions
{
    extension(string? text)
    {
        public IEnumerable<string>? SplitLines(bool cutEmpty)
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
    }

    extension(PlantUmlText? ptext)
    {
        public IEnumerable<string> SplitLines(bool cutEmpty)
        {
            var ptextText = ptext?.Text;
            return ptextText?.SplitLines(cutEmpty) ?? [];
        }
    }

    extension<TArgument>(IEnumerable<TArgument> src)
    {
        public HashSet<TArgument> ToHashSet()
        {
            var result = new HashSet<TArgument>();
            foreach (var i in src)
                result.Add(i);
            return result;
        }
    }
}
