using System;
using System.Linq;
using System.Text;

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
                var bt = t.BaseType.MeOrGeneric();
                if (t.BaseType != null && diagram.ContainsType(bt))
                {
                    var rel = Inherits(bt, t, diagram).With(UmlArrowDirections.Up);
                    var a1 = t.BaseType.GetGenericTypeArgumentsIfPossible();
                    if (a1.Length > 0)
                    {
                        var a4 = t.BaseType.MeOrGeneric().GetGenericArguments();
                        var sb = new StringBuilder();
                        for (int i = 0; i < a4.Length; i++)
                        {
                            var t1 = a1[i];
                            var t2 = a4[i];
                            var txt = t2.Name + "=" + diagram.GetTypeName(t1);
                            if (i > 0)
                                sb.Append(", ");
                            sb.Append(txt);
                        }

                        rel.Label = sb.ToString();
                    }                    
                    diagram.Relations.Add(rel);
                }

                foreach (var i in t.GetInterfaces().Select(a=>a.MeOrGeneric()))
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