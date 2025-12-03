namespace AutoUml;

public class UmlAddRelationAttributeVisitor : NewTypeMultipleAttributeVisitor<UmlAddRelationAttribute>
{
    protected override void VisitInternal(UmlDiagram diagram, UmlEntity info, UmlAddRelationAttribute att)
    {
        var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(info.Type)),
                Right = new UmlRelationEnd(diagram.GetTypeName(att.RelatedType)),
                Arrow = UmlRelationArrow.MkArrow(att, att.Multiple),
                Label = att.Name
            }
            .WitCreatorMeta<UmlAddRelationAttributeVisitor>(info.Type, att.RelatedType)
            .WithNote(att);
        rel.Tag = att.Tag;
        diagram.Relations.Add(rel);
        if (att.ForceAddToDiagram)
            diagram.UpdateTypeInfo(att.RelatedType, null);
    }
}