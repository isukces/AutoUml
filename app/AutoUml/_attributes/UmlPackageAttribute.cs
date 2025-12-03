using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities)]
[Conditional("AUTOUML_ANNOTATIONS")]
public class UmlPackageAttribute : Attribute
{
    public UmlPackageAttribute(string packageName)
    {
        PackageName = packageName;
    }

    public string PackageName { get; }
}
