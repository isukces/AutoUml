namespace AutoUml
{
    public abstract class UmlMember
    {
        public abstract void WriteTo(CodeWriter cf, UmlProjectDiagram diagram);
        public int    Group      { get; set; }
        public string Name       { get; set; }
        public bool   HideOnList { get; set; }
    }
}