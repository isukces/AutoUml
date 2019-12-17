using System;
using System.Collections.Generic;
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
                var entityType = e.Type;
                var bt         = entityType.BaseType.MeOrGeneric();
                if (entityType.BaseType != null && diagram.ContainsType(bt))
                {
                    var rel = Inherits(bt, entityType, diagram).With(UmlArrowDirections.Up);
                    var a1  = entityType.BaseType.GetGenericTypeArgumentsIfPossible();
                    if (a1.Length > 0)
                    {
                        var a4 = entityType.BaseType.MeOrGeneric().GetGenericArguments();
                        var sb = new StringBuilder();
                        for (var i = 0; i < a4.Length; i++)
                        {
                            var t1  = a1[i];
                            var t2  = a4[i];
                            var txt = t2.Name + "=" + diagram.GetTypeName(t1);
                            if (i > 0)
                                sb.Append(", ");
                            sb.Append(txt);
                        }

                        rel.Label = sb.ToString();
                    }

                    diagram.Relations.Add(rel);
                }

                bool CanAddR(Type clasType, Type interfaceType)
                {
                    clasType = clasType.BaseType;
                    while (clasType != null)
                    {
                        if (!diagram.ContainsType(clasType))
                            return true;
                        if (clasType.GetInterfaces().Contains(interfaceType))
                            return false;
                    }

                    return true;
                }

                var entityInterfaces = entityType
                    .GetInterfaces()
                    .Select(a => a.MeOrGeneric())
                    .ToHashSet();
                var hideRelationToInterface = new HashSet<Type>();
                foreach (var entityInterface in entityInterfaces)
                {
                    var baseInterfaces = entityInterface.GetInterfaces();
                    foreach (var baseI in baseInterfaces)
                        if (diagram.ContainsType(baseI))
                            hideRelationToInterface.Add(baseI);
                }

                foreach (var i in hideRelationToInterface)
                    entityInterfaces.Remove(i);
                foreach (var interfaceType in entityInterfaces)
                {
                    if (!diagram.ContainsType(interfaceType)) continue;
                    if (!CanAddR(entityType, interfaceType)) continue;
                    var umlRelation = Inherits(interfaceType, entityType, diagram)
                        .With(UmlArrowDirections.Up);
                    diagram.Relations.Add(umlRelation);
                }
            }
        }


        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }
    }
}