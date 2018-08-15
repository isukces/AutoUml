using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        private static IEnumerable<Type> ProcessMethod(UmlDiagram diagram, UmlEntity diagClass, MethodUmlMember member)
        {
            if (diagClass.Type != member.Method.DeclaringType)
            {
                if (diagram.ContainsType(diagClass.Type.BaseType.MeOrGeneric()))
                {
                    member.HideOnList = true;
                    yield break;
                }
            }

            var att = member.Method.GetCustomAttribute<UmlRelationAttribute>();
            if (att == null)
                yield break;

            var ti = new TypeExInfo(att.ForceType ?? member.Method.ReturnType);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation

            member.HideOnList = true;

            var          owner          = diagClass.Type;
            var          component      = ti.ElementType;
            const string ownerLabel     = "";
            const string componentLabel = "";

            var arrow = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple ?? ti.IsCollection);
            if (att.ArrowDirection != UmlArrowDirections.Auto)
                arrow.ArrowDirection = att.ArrowDirection;
            if (att.ForceAddToDiagram)
                yield return ti.ElementType;

            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(diagram.GetTypeName(component), componentLabel),
                Arrow = arrow,
                Label = GetLabel(member)
            }.WithNote(att);
            diagram.Relations.Add(rel);
        }

        private static IEnumerable<Type> ProcessProperty(UmlDiagram diagram, UmlEntity diagClass,
            PropertyUmlMember prop)
        {
            if (diagClass.Type != prop.Property.DeclaringType)
            {
                if (diagram.ContainsType(diagClass.Type.BaseType))
                {
                    prop.HideOnList = true;
                    yield break;
                }
            }

            var ti = new TypeExInfo(prop.Property.PropertyType);
            if (!diagram.ContainsType(ti.ElementType)) yield break;
            // create relation

            prop.HideOnList = true;
            var arrow = new UmlRelationArrow(
                ArrowEnd.Empty,
                ti.IsCollection ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen);
            var          owner          = diagClass.Type;
            var          component      = ti.ElementType;
            const string ownerLabel     = "";
            const string componentLabel = "";

            var att = prop.Property.GetCustomAttribute<UmlRelationAttribute>();
            if (att != null)
            {
                var relationTi = new TypeExInfo(att.ForceType ?? prop.Property.PropertyType);
                arrow = UmlRelationArrow.GetRelationByKind(att.Kind, att.Multiple ?? relationTi.IsCollection);
                if (att.ArrowDirection != UmlArrowDirections.Auto)
                    arrow.ArrowDirection = att.ArrowDirection;
                if (att.ForceAddToDiagram)
                    yield return relationTi.ElementType;
            }

            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(diagram.GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(diagram.GetTypeName(component), componentLabel),
                Arrow = arrow,
                Label = prop.Name
            }.WithNote(att);
            diagram.Relations.Add(rel);
        }

        public void VisitBeforeEmit(UmlDiagram diagram)
        {
            var typesToAdd = new List<Type>();
            foreach (var diagClass in diagram.GetEntities())
            {
                foreach (var mem in diagClass.Members)
                {
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
                }
            }

            foreach (var i in typesToAdd)
            {
                diagram.UpdateTypeInfo(i, null);
            }
        }

        public void VisitDiagramCreated(UmlDiagram diagram)
        {
        }
    }
}