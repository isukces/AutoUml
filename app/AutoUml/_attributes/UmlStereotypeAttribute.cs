using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities)]
[Conditional("AUTOUML_ANNOTATIONS")]
public class UmlStereotypeAttribute : Attribute
{
    public UmlStereotypeAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
