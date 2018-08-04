namespace AutoUml
{
    public interface IDiagramVisitor
    {
        void VisitBeforeEmit(UmlDiagram diagram);
        void VisitDiagramCreated(UmlDiagram diagram);
    }
}