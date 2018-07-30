using System.Linq;
using System.Reflection;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class MemberToRelationVisitor : IDiagramVisitor
    {
        public void VisitBeforeEmit(UmlProjectDiagram diagram)
        {
            foreach (var diagClass in diagram.GetEntities())
            {
                foreach (var prop in diagClass.Members.OfType<PropertyUmlMember>())
                {
                    if (diagClass.Type != prop.Property.DeclaringType)
                    {
                        if (diagram.ContainsType(diagClass.Type.BaseType))
                        {
                            prop.HideOnList = true;
                            continue;
                        }
                    }

                    var ti = new TypeExInfo(prop.Property.PropertyType);
                    if (!diagram.ContainsType(ti.ElementType)) continue;
                    // create relation

                    prop.HideOnList = true;
                    var arrow = new UmlRelationArrow(
                        ArrowEnd.Empty,
                        ti.IsCollection ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen);
                    var owner          = diagClass.Type;
                    var component      = ti.ElementType;
                    var ownerLabel     = "";
                    var componentLabel = "";

                    {
                        var att = prop.Property.GetCustomAttribute<UmlRelationAttribute>();
                        if (att != null)
                        {
                            arrow = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple ?? ti.IsCollection);
                            if (att.ArrowDirection != UmlArrowDirections.Auto)
                                arrow.ArrowDirection = att.ArrowDirection;
                        }
                    }

                    var rel = new UmlRelation
                    {
                        Left  = new UmlRelationEnd(diagram.GetTypeName(owner), ownerLabel),
                        Right = new UmlRelationEnd(diagram.GetTypeName(component), componentLabel),
                        Arrow = arrow,
                        Label = prop.Name
                    };
                    diagram.Relations.Add(rel);
                }
            }
        }

        public void VisitDiagramCreated(UmlProjectDiagram diagram)
        {
        }
    }
}