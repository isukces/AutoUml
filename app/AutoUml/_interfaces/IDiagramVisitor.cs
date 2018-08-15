namespace AutoUml
{
    public interface IDiagramVisitor:IUmlVisitor
    {
        void VisitBeforeEmit(UmlDiagram diagram);
        void VisitDiagramCreated(UmlDiagram diagram);
    }
}