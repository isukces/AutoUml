using System;
using System.Diagnostics;

namespace AutoUml;

[Conditional("AUTOUML_ANNOTATIONS")]
public class UmlAddMetaAttribute : Attribute
{
    public UmlAddMetaAttribute(string name, string valueString)
    {
        Name        = name;
        ValueString = valueString;
    }

    public string Name        { get; }
    public string ValueString { get; }
}
