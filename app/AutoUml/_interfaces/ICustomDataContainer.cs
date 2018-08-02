using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public interface ICustomDataContainer
    {
        
        [NotNull]
        Dictionary<string, object> CustomData { get; }
    }
}