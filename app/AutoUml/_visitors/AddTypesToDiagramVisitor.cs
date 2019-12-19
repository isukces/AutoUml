namespace AutoUml
{
    public sealed class AddTypesToDiagramVisitor : NewTypeSingleAttributeVisitor<UmlAddTypesToDiagramAttribute>
    {
        protected override void VisitInternal(UmlDiagram diagram, UmlEntity info, UmlAddTypesToDiagramAttribute att)
        {
            if (att.Types == null || att.Types.Length == 0)
                return;
            foreach (var type in att.Types)
                diagram.UpdateTypeInfo(type, null);
        }
    }
}