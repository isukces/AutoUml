using System;

namespace AutoUml
{
    public class AddInheritRelationVisitor : IDiagramVisitor
    {
        private static UmlRelation Inherits(Type baseClass, Type subClass, UmlDiagram diagram)
        {
            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(subClass)),
                Right = new UmlRelationEnd(diagram.GetTypeName(baseClass)),
                Arrow = UmlRelationArrow.InheritRight
            };
            return rel;
        }


        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            foreach (var e in diagram.GetEntities())
            {
                var t = e.Type;
                var bt = t.BaseType;
                if (bt != null && bt.IsGenericType)
                    bt = bt.GetGenericTypeDefinition();
                if (t.BaseType != null && diagram.ContainsType(bt))
                {
                    diagram.Relations.Add(Inherits(bt, t, diagram).With(UmlArrowDirections.Up));
                }

                foreach (var i in t.GetInterfaces())
                {
                    if (diagram.ContainsType(i))
                        diagram.Relations.Add(Inherits(i, t, diagram).With(UmlArrowDirections.Up));
                }
            }
        }


        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }
    }
}