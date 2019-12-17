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

        private static string GetLabel(MethodUmlMember member)
        {
            var argsCollection = member.Method.GetParameters()
                .Select(a => a.Name);
            var args = string.Join(", ", argsCollection);
            return $"{member.Name}({args})";
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

            var ti = new TypeExInfo(prop.Property.PropertyType, false);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation

            prop.HideOnList = true;
            var arrow = new UmlRelationArrow(
                ArrowEnd.Empty,
                ti.IsCollection ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen);
            var          owner          = diagClass.Type;
            var          arrowTargetType      = ti.ElementType;
            const string ownerLabel     = "";
            const string componentLabel = "";

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