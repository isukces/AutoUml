using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Tests;

internal static class TestUtils
{
    extension(object obj)
    {
        public string ToCompareJson()
        {
            var sb = new StringBuilder();
            using(var sw = new StringWriter(sb))
            using(var writer = new JsonTextWriter(sw))
            {
                writer.QuoteChar = '\'';

                var ser = new JsonSerializer();
                ser.Serialize(writer, obj);
            }

            return sb.ToString();
        }
    }
}
