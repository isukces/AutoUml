using System;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities)]
public class UmlPackageAttribute : Attribute
{
    public UmlPackageAttribute(string packageName)
    {
        PackageName = packageName;
    }

    public string PackageName { get; }
}
