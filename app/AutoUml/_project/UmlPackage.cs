namespace AutoUml
{
    public class UmlPackage
    {
        public UmlPackageKind Kind { get; set; }
    }

    public enum UmlPackageKind
    {
        Node,
        Rectangle,
        Folder,
        Frame,
        Cloud,
        Database
    }
}