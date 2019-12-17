using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class MemberToRelationVisitor : IDiagramVisitor
    {
        private static string GetLabel(MethodUmlMember member)
        {
            var argsCollection = member.Method.GetParameters()
                .Select(a => a.Name);
            var args = string.Join(", ", argsCollection);
            return $"{member.Name}({args})";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetMultiplicity(Multiplicity kind, bool isCollection)
        {
            switch (kind)
            {
                case Multiplicity.Auto:
                    return isCollection;
                case Multiplicity.Multiple:
                    return true;
                case Multiplicity.Single:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private static IEnumerable<Type> ProcessMethod(UmlDiagram diagram, UmlEntity diagClass, MethodUmlMember member)
        {
            if (diagClass.Type != member.Method.DeclaringType)
                if (diagram.ContainsType(diagClass.Type.BaseType.MeOrGeneric()))
                {
                    member.HideOnList = true;
                    yield break;
                }

            var att = member.Method.GetCustomAttribute<UmlRelationAttribute>();
            if (att == null)
                yield break;

            var ti = new TypeExInfo(att.RelatedType ?? member.Method.ReturnType, att.DoNotResolveCollections);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation

            member.HideOnList = true;

            var          owner          = diagClass.Type;
            var          component      = ti.ElementType;
            const string ownerLabel     = "";
            const string componentLabel = "";

            var arrow = UmlRelationArrow.MkArrow(att, GetMultiplicity(att.Multiple, ti.IsCollection));
            if (att.ForceAddToDiagram)
                yield return ti.ElementType;

            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(diagram.GetTypeName(component), componentLabel),
                Arrow = arrow,
                Label = string.IsNullOrEmpty(att.Name) ? GetLabel(member) : att.Name
            }.WithNote(att);
            diagram.Relations.Add(rel);
        }

        private static IEnumerable<Type> ProcessProperty(UmlDiagram diagram, UmlEntity diagClass,
            PropertyUmlMember prop)
        {
            if (prop.Property.GetCustomAttribute<DontConvertToRelationAttribute>() != null)
                yield break;
            if (diagClass.Type != prop.Property.DeclaringType)
                if (diagram.ContainsType(diagClass.Type.BaseType))
                {
                    prop.HideOnList = true;
                    yield break;
                }

            var doNotResolveCollections =
                prop.Property.GetCustomAttribute<BaseRelationAttribute>()?.DoNotResolveCollections ?? false;
            var ti = new TypeExInfo(prop.Property.PropertyType, doNotResolveCollections);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation
            if (!CanAddRelation(diagram, prop))
                yield break;
            prop.HideOnList = true;
            var arrow = new UmlRelationArrow(
                ArrowEnd.Empty,
                ti.IsCollection ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen);
            var          owner           = diagClass.Type;
            var          arrowTargetType = ti.ElementType;
            const string ownerLabel      = "";
            const string componentLabel  = "";

            var att = prop.Property.GetCustomAttribute<UmlRelationAttribute>();
            if (att != null)
            {
                var relationTi = new TypeExInfo(att.RelatedType ?? prop.Property.PropertyType,
                    att.DoNotResolveCollections);
                arrow = UmlRelationArrow.MkArrow(att, GetMultiplicity(att.Multiple, relationTi.IsCollection));
                if (att.ForceAddToDiagram)
                    yield return relationTi.ElementType;
                arrowTargetType = relationTi.ElementType;
            }

            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(diagram.GetTypeName(arrowTargetType), componentLabel),
                Arrow = arrow,
                Label = string.IsNullOrEmpty(att?.Name) ? prop.Name : att.Name
            }.WithNote(att);
            diagram.Relations.Add(rel);
        }

        private static bool CanAddRelation(UmlDiagram diagram, PropertyUmlMember prop)
        {
            var reflectedType = prop.Property.ReflectedType;

            
            var interfaces = reflectedType.GetInterfaces();
            var propertyName = prop.Property.Name;
            foreach (var intf in interfaces)
                if (diagram.ContainsType(intf))
                {
                    var map = reflectedType.GetInterfaceMap(intf);
                    foreach (var i in map.TargetMethods)
                    {
                        if (!IsPropertyMethod(i, propertyName)) continue;
                        // don't add relation if there is interface on the diagrarm with same relation
                        if (prop.Property.GetMethod == i || prop.Property.SetMethod == i)
                            return false;
                    }
                }

            var declaringType = prop.Property.DeclaringType;
            if (reflectedType != declaringType)
            {
                var tt = reflectedType.BaseType;
                while (tt != null)
                {
                    if (diagram.ContainsType(tt)) 
                        return false;
                    if (tt == declaringType)
                        break;
                    tt = tt.BaseType;
                }
            }

            return true;
        }

        private static bool IsPropertyMethod(MethodInfo a, string propertyName)
        {
            return a.IsSpecialName && (a.Name == "get_" + propertyName || a.Name=="set_"+propertyName);
        }

        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            var typesToAdd = new List<Type>();
            foreach (var diagClass in diagram.GetEntities())
            foreach (var mem in diagClass.Members)
                switch (mem)
                {
                    case MethodUmlMember methodUmlMember:
                        var types1 = ProcessMethod(diagram, diagClass, methodUmlMember);
                        typesToAdd.AddRange(types1);
                        break;
                    case PropertyUmlMember propertyUmlMember:
                        var types2 = ProcessProperty(diagram, diagClass, propertyUmlMember);
                        typesToAdd.AddRange(types2);
                        break;
                }

            foreach (var i in typesToAdd)
                diagram.UpdateTypeInfo(i, null);
        }

        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }
    }
}