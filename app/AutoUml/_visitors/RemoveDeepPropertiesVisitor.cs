using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUml;

/// <summary>
///     Removes deep properties i.e. declared in Canvas or Panel
///     It is no standard visitor
/// </summary>
public class RemoveDeepPropertiesVisitor : INewTypeInDiagramVisitor
{
    public RemoveDeepPropertiesVisitor(params Type[]? types)
    {
        if (types == null) return;
        for (var index = 0; index < types.Length; index++)
            AddTypeAndParents(types[index]);
    }

    public void AddTypeAndParents(Type type)
    {
        type = type.MeOrGeneric();
        while (type != null)
        {
            _types.Add(type);
            type = type.BaseType.MeOrGeneric();
        }
    }

    private bool ShouldBeHidden(MemberInfo? memberInfo)
    {
        if (memberInfo == null)
            return false;
        var type = memberInfo.DeclaringType.MeOrGeneric();
        return _types.Contains(type);
    }

    public void Visit(UmlDiagram diagram, UmlEntity info)
    {
        for (var index = 0; index < info.Members.Count; index++)
        {
            var umlMember  = info.Members[index];
            var memberInfo = umlMember.GetMemberInfo();
            var hide       = ShouldBeHidden(memberInfo);
            if (hide)
                umlMember.HideOnList = true;
        }
    }

    private readonly HashSet<Type> _types = new HashSet<Type>();
}
