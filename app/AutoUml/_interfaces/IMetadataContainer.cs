using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public interface IMetadataContainer
    {
        [NotNull]
        Dictionary<string, object> Metadata { get; }
    }

    public static class MetadataContainerExtensions
    {
        public static string TryGetStringMetadata(this IMetadataContainer container, string key)
        {
            if (container == null)
                return null;
            if (!container.Metadata.TryGetValue(key, out var x)) return null;
            switch (x)
            {
                case string s: return s;
                case null: return null;
            }

            return x.ToString();
        }
    }
}