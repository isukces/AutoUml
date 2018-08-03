using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public interface IMetadataContainer
    {
        
        [NotNull]
        Dictionary<string, object> Metadata { get; }
    }
}