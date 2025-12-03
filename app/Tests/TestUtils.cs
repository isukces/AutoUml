using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Tests;

static class TestUtils
{
    public static string ToCompareJson(this object obj)
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