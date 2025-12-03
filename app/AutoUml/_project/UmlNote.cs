using System.Collections.Generic;

namespace AutoUml;

public class UmlNote : IMetadataContainer
{
    public IUmlFill Background { get; set; }
    public string   Text       { get; set; }

    public Dictionary<string, object> Metadata { get; } = new Dictionary<string, object>();
}
