using System.Reflection;

namespace AutoUml
{
    public class UmlAddRelationAttributeVisitor : INewTypeInDiagramVisitor
    {
        public void Visit(UmlProjectDiagram diagram, UmlEntity info)
        {
            var type = info.Type;
            foreach (var att in type.GetCustomAttributes<UmlAddRelationAttribute>())
            {
                INoteProvider np = att;
                var rel = new UmlRelation
                {
                    Left  = new UmlRelationEnd(diagram.GetTypeName(type)),
                    Right = new UmlRelationEnd(diagram.GetTypeName(att.RelatedType)),
                    Arrow = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple),
                    Label = att.Name,
                }
                .WithNote(att);
                diagram.Relations.Add(rel);
            }
        }
    }
}