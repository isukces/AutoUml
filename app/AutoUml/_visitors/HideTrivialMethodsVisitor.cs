using System;
using JetBrains.Annotations;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class HideTrivialMethodsVisitor : IDiagramVisitor
    {
        [UsedImplicitly]
        public static bool? DefaultHideMethod(UmlDiagram diagram, UmlMember umlMember, UmlEntity entity)
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

            if (!(umlMember is MethodUmlMember mum))
                return null;
            var mi = mum.Method;
            if (mi.DeclaringType == typeof(object))
                return true;
            if (AlreadyOnDiagram(entity.Type, mi.DeclaringType))
                return true;

            return null;
        }

        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            foreach (var entity in diagram.GetEntities())
            foreach (var me in entity.Members)
            {
                var hide = HideMember?.Invoke(diagram, me, entity);
                if (hide is null)
                    hide = DefaultHideMethod(diagram, me, entity);
                if (hide != null)
                    me.HideOnList = hide.Value;
            }
        }

        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }

        public Func<UmlDiagram, UmlMember, UmlEntity, bool?> HideMember { get; set; }
    }
}