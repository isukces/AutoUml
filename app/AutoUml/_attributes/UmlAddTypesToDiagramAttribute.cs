using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
[Conditional("AUTOUML_ANNOTATIONS")]
public sealed class UmlAddTypesToDiagramAttribute : Attribute
{
    public UmlAddTypesToDiagramAttribute(params Type[] types)
    {
        Types = types;
    }

    public Type[] Types { get; }
}
