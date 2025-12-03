namespace AutoUml;

public class UmlPackageAttributeVisitor : NewTypeSingleAttributeVisitor<UmlPackageAttribute>
{
    protected override void VisitInternal(UmlDiagram diagram, UmlEntity info, UmlPackageAttribute att)
    {
        info.PackageName = att.PackageName;
    }
}
