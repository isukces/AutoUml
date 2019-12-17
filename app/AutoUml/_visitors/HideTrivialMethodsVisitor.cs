using System;
using System.Reflection;
using JetBrains.Annotations;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class HideTrivialMethodsVisitor : IDiagramVisitor
    {
        public static bool IsShouldSerializeMethod(MethodInfo mi)
        {
            if (mi.Name.StartsWith("ShouldSerialize"))
                if (mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0)
                    return true;
            return false;
        }

        [UsedImplicitly]
        public SetFlagResult DefaultHideMethod(UmlDiagram diagram, UmlMember umlMember, UmlEntity entity)
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
                return SetFlagResult.LeaveUnchanged;
            var mi = mum.Method;
            if (mi.DeclaringType == typeof(object))
                return SetFlagResult.SetToTrue;
            if (AlreadyOnDiagram(entity.Type, mi.DeclaringType))
                return SetFlagResult.SetToTrue;

            if (HideShouldSerializeMethods)
                if (IsShouldSerializeMethod(mi))
                    return SetFlagResult.SetToTrue;

            return SetFlagResult.LeaveUnchanged;
        }

        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            foreach (var entity in diagram.GetEntities())
            foreach (var me in entity.Members)
            {
                var hide = HideMember?.Invoke(diagram, me, entity) ?? SetFlagResult.LeaveUnchanged;

                if (hide == SetFlagResult.LeaveUnchanged)
                    hide = DefaultHideMethod(diagram, me, entity);
                if (hide != SetFlagResult.LeaveUnchanged)
                    me.HideOnList = hide == SetFlagResult.SetToTrue;
            }
        }

        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }

        public CheckHideMemeberDelegate HideMember { get; set; }

        /// <summary>
        ///     Hides bool ShouldSerializeXXX() methods usually used for Json serialization
        /// </summary>
        public bool HideShouldSerializeMethods { get; set; }
    }

    public delegate SetFlagResult CheckHideMemeberDelegate(UmlDiagram diagram, UmlMember umlMember, UmlEntity entity);

    public enum SetFlagResult
    {
        LeaveUnchanged,
        SetToTrue,
        SetToFalse
    }
}