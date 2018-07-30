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
                var rel = new UmlRelation
                {
                    Left      = new UmlRelationEnd(diagram.GetTypeName(type)),
                    Right     = new UmlRelationEnd(diagram.GetTypeName(att.RelatedType)),
                    Arrow     = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple),
                    Label     = att.Name,
                    Note      = att.Note,
                    NoteColor = att.NoteColor
                };
                diagram.Relations.Add(rel);
            }
        }
    }
}