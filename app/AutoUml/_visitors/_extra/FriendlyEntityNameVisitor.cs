namespace AutoUml
{
    public class FriendlyEntityNameVisitor : INewTypeInDiagramVisitor
    {
        public FriendlyEntityNameVisitor(bool onlyFirstUpper = false)
        {
            OnlyFirstUpper = onlyFirstUpper;
        }

        public void Visit(UmlDiagram diagram, UmlEntity info)
        {
            if (info.Name == info.Type.Name)
                info.Name = info.Name.CamelToNormal(OnlyFirstUpper);
        }

        public bool OnlyFirstUpper { get; set; }
    }
}