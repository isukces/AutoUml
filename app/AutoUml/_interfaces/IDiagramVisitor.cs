namespace AutoUml
{
    public interface IDiagramVisitor
    {
        void VisitBeforeEmit(UmlProjectDiagram diagram);
        void VisitDiagramCreated(UmlProjectDiagram diagram);
    }
}