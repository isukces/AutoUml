namespace AutoUml;

/// <summary>
///     Adds to diagram interfaces implemented by class
/// </summary>
public sealed   class  UmlAddImplementedInterfacesToDiagramAttributeVisitor : 
    NewTypeSingleAttributeVisitor<UmlAddImplementedInterfacesToDiagramAttribute>
{
    protected override void VisitInternal(UmlDiagram diagram, UmlEntity info,
        UmlAddImplementedInterfacesToDiagramAttribute att)
    {
        var typesToAdd = info.Type.GetInterfaces();
        foreach (var type in typesToAdd)
            diagram.UpdateTypeInfo(type, null);
    }
}