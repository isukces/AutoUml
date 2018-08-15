using System.Reflection;

namespace AutoUml
{
    /// <summary>
    ///     Converts class members into relations
    /// </summary>
    public class ForceAddToDiagramVisitor : INewTypeInDiagramVisitor
    {
        private static TypeExInfo GetTi(MemberInfo memberInfo)
        {
            var att = memberInfo.GetCustomAttribute<UmlRelationAttribute>();
            if (att == null || !att.ForceAddToDiagram) return null;
            if (att.ForceType != null)
                return new TypeExInfo(att.ForceType);
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    return new TypeExInfo(fieldInfo.FieldType);
                case MethodInfo mi:
                    if (mi == typeof(void))
                        return null;
                    return new TypeExInfo(mi.ReturnType);
                case PropertyInfo pi:
                    return new TypeExInfo(pi.PropertyType);
            }

            return null;
        }

        public void Visit(UmlDiagram diagram, UmlEntity info)
        {
            var members = info.Type.GetMembers(MyBindingFlags);
            foreach (var memberInfo in members)
            {
                var typeExInfo = GetTi(memberInfo);
                if (typeExInfo != null)
                    diagram.UpdateTypeInfo(typeExInfo.ElementType, null);
            }
        }

        public BindingFlags MyBindingFlags { get; set; } = BindingFlags.Instance | BindingFlags.Static
                                                                                 | BindingFlags.Public;
    }
}