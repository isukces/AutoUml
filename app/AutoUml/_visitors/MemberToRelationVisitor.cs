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
        private static bool CanAddRelation(UmlDiagram diagram, PropertyUmlMember prop)
        {
            var reflectedType = prop.Property.ReflectedType;

            var interfaces   = reflectedType.GetInterfaces();
            var propertyName = prop.Property.Name;
            if (reflectedType.IsInterface)
                return true;
            foreach (var intf in interfaces)
                if (diagram.ContainsType(intf))
                {
                    //
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

        private static bool IsPropertyMethod(MethodInfo a, string propertyName)
        {
            return a.IsSpecialName && (a.Name == "get_" + propertyName || a.Name == "set_" + propertyName);
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
            rel.Tag        = att.Tag;
            rel.BaseMember = member;
            diagram.Relations.Add(rel);
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

        private IEnumerable<Type> ProcessProperty(UmlDiagram diagram, UmlEntity diagClass,
            PropertyUmlMember property)
        {
            var decision = ConvertToRelation?.Invoke(property) ?? ChangeDecision.Auto;
            if (decision == ChangeDecision.Auto)
            {
                if (property.Property.GetCustomAttribute<DontConvertToRelationAttribute>() != null)
                    decision = ChangeDecision.No;
                else
                    decision = ChangeDecision.Yes;
            }

            if (decision == ChangeDecision.No)
                yield break;

            if (diagClass.Type != property.Property.DeclaringType)
                if (diagram.ContainsType(diagClass.Type.BaseType))
                {
                    property.HideOnList = true;
                    yield break;
                }

            var doNotResolveCollections =
                property.Property.GetCustomAttribute<BaseRelationAttribute>()?.DoNotResolveCollections ?? false;
            var ti = new TypeExInfo(property.Property.PropertyType, doNotResolveCollections);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation
            if (!CanAddRelation(diagram, property))
                yield break;
            property.HideOnList = true;
            var arrow = new UmlRelationArrow(
                ArrowEnd.Empty,
                ti.IsCollection ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen);
            var          owner           = diagClass.Type;
            var          arrowTargetType = ti.ElementType;
            const string ownerLabel      = "";
            const string componentLabel  = "";

            var att = property.Property.GetCustomAttribute<UmlRelationAttribute>();
            if (att != null)
            {
                var relationTi = new TypeExInfo(att.RelatedType ?? property.Property.PropertyType,
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
                Label = string.IsNullOrEmpty(att?.Name) ? property.Name : att.Name
            }.WithNote(att);
            rel.Tag        = att?.Tag;
            rel.BaseMember = property;
            {
                var eventHandler = AfterConversionProperty;
                if (eventHandler != null)
                {
                    var args = new AfterConversionPropertyEventArgs
                    {
                        Diagram       = diagram,
                        Entity        = diagClass,
                        BaseUmlMember = property,
                        Relation      = rel
                    };
                    eventHandler(this, args);
                }
            }
            diagram.Relations.Add(rel);
        }

        public ConvertToRelationDelegate                            ConvertToRelation { get; set; }
        public event EventHandler<AfterConversionPropertyEventArgs> AfterConversionProperty;

        public class AfterConversionPropertyEventArgs
        {
            public UmlDiagram        Diagram       { get; set; }
            public UmlEntity         Entity        { get; set; }
            public PropertyUmlMember BaseUmlMember { get; set; }
            public UmlRelation       Relation      { get; set; }
        }
    }

    public delegate ChangeDecision ConvertToRelationDelegate(PropertyUmlMember property);

    public enum ChangeDecision
    {
        Auto,
        Yes,
        No
    }
}