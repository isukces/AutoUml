using System;
using System.Linq;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class HideTrivialMethodsVisitor : IDiagramVisitor
    {
        public void VisitBeforeEmit(UmlProjectDiagram diagram)
        {
            bool AlreadyOnDiagram(Type entityType, Type declaringType)
            {
                if (entityType == null || entityType == declaringType)
                    return false;
                while (true)
                {
                    if (entityType.BaseType == null)
                        return false;
                    if (diagram.ContainsType(entityType.BaseType))
                        return true;
                    entityType = entityType.BaseType;
                }
            }

            foreach (var entity in diagram.GetEntities())
            {
                foreach (var m in entity.Members.OfType<MethodUmlMember>())
                {
                    var mi = m.Method;
                    if (mi.DeclaringType == typeof(object))
                        m.HideOnList = true;
                    else if (AlreadyOnDiagram(entity.Type, mi.DeclaringType))
                        m.HideOnList = true;
                }
            }
        }

        public void VisitDiagramCreated(UmlProjectDiagram diagram)
        {
        }
    }
}