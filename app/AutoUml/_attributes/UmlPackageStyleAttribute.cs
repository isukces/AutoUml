using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
[Conditional("AUTOUML_ANNOTATIONS")]
public class UmlPackageStyleAttribute : Attribute
{
    public UmlPackageStyleAttribute(string packageName, UmlPackageKind kind, string? diagramName = null)
    {
        PackageName = packageName;
        Kind        = kind;
        DiagramName = diagramName;
    }

    public bool CanBeUsedFor(UmlDiagram? diagram)
    {
        return string.IsNullOrEmpty(DiagramName)
               || string.Equals(DiagramName, diagram?.Name.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    public string         PackageName { get; }
    public UmlPackageKind Kind        { get; }

    public string? DiagramName
    {
        get;
        set => field = value?.Trim();
    }
}
