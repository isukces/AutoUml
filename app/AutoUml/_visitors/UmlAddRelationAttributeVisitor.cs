using System.Reflection;

namespace AutoUml
{
    public class UmlAddRelationAttributeVisitor : NewTypeMultipleAttributeVisitor<UmlAddRelationAttribute>
    {
        protected override void VisitInternal(UmlProjectDiagram diagram, UmlEntity info, UmlAddRelationAttribute att)
        {
            var rel = new UmlRelation
                {
                    Left  = new UmlRelationEnd(diagram.GetTypeName(info.Type)),
                    Right = new UmlRelationEnd(diagram.GetTypeName(att.RelatedType)),
                    Arrow = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple),
                    Label = att.Name
                }
                .WithNote(att);
            diagram.Relations.Add(rel);
        }
    }
}