namespace AutoUml
{
    /// <summary>
    ///     Visitor used when new type is added to diagram
    /// </summary>
    public interface INewTypeInDiagramVisitor
    {
        void Visit(UmlDiagram diagram, UmlEntity info);
    }
}