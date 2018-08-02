using System.Collections.Generic;

namespace AutoUml
{
    public class UmlNote : ICustomDataContainer
    {
        public IUmlFill                   Background { get; set; }
        public string                     Text       { get; set; }
        public Dictionary<string, object> CustomData { get; } = new Dictionary<string, object>();
    }
}