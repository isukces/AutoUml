using System;

namespace AutoUml;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
public sealed class UmlAddTypesToDiagramAttribute : Attribute
{
    public UmlAddTypesToDiagramAttribute(params Type[] types)
    {
        Types = types;
    }

    public Type[] Types { get; }
}
