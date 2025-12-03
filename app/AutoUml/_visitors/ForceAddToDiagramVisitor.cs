using System.Reflection;

namespace AutoUml;

/// <summary>
///     Converts class members into relations
/// </summary>
public class ForceAddToDiagramVisitor : INewTypeInDiagramVisitor
{
    private static TypeExInfo? GetTi(MemberInfo memberInfo)
    {
        var att = memberInfo.GetCustomAttribute<BaseRelationAttribute>();
        if (att == null || !att.ForceAddToDiagram) return null;
        if (att.RelatedType != null)
            return new TypeExInfo(att.RelatedType, att.DoNotResolveCollections);
        switch (memberInfo)
        {
            case FieldInfo fieldInfo:
                return new TypeExInfo(fieldInfo.FieldType, att.DoNotResolveCollections);
            case MethodInfo mi:
                if (mi == typeof(void))
                    return null;
                return new TypeExInfo(mi.ReturnType, att.DoNotResolveCollections);
            case PropertyInfo pi:
                return new TypeExInfo(pi.PropertyType, att.DoNotResolveCollections);
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