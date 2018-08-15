using System;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class HideTrivialMethodsVisitor : IDiagramVisitor
    {
        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            bool AlreadyOnDiagram(Type entityType, Type declaringType)
            {
                if (entityType == null || entityType == declaringType)
                    return false;
                while (true)
                {
                    var bt = entityType.BaseType.MeOrGeneric();
                    if (bt == null)
                        return false;
                    if (diagram.ContainsType(bt))
                        return true;
                    entityType = bt;
                }
            }

            foreach (var entity in diagram.GetEntities())
            {
                foreach (var me in entity.Members)
                {
                    var hide = HideMember?.Invoke(me);
                    if (hide != null)
                        me.HideOnList = hide.Value;
                    else if (me is MethodUmlMember m)
                    {
                        var mi = m.Method;
                        if (mi.DeclaringType == typeof(object))
                            m.HideOnList = true;
                        else if (AlreadyOnDiagram(entity.Type, mi.DeclaringType))
                            m.HideOnList = true;
                    }
                }
            }
        }

        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }

        public Func<UmlMember, bool?> HideMember { get; set; }
    }
}