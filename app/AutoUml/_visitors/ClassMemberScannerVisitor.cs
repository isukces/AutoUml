using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AutoUml;

[Flags]
public enum ReflectionFlags
{
    None = 0,
    PublicMethod = 1,
    ProtectedMethod = 2,
    PrivateMethod = 4,
    StaticMethod = 8,
    InstanceMethod = 16,
    PublicSetterProperty = 32,
    ProtectedSetterProperty = 64,
    PrivateSetterProperty = 128,
    PublicGetterProperty = 256,
    ProtectedGetterProperty = 512,
    PrivateGetterProperty = 1024,
    StaticProperty = 2048,
    InstanceProperty = 4096,

    All = 8192 - 1,
    //================================================


    Method = PublicMethod |
             ProtectedMethod |
             PrivateMethod |
             StaticMethod |
             InstanceMethod,


    PublicProperty = PublicSetterProperty | PublicGetterProperty,
    ProtectedProperty = ProtectedSetterProperty | ProtectedGetterProperty,
    PrivateProperty = PrivateSetterProperty | PrivateGetterProperty,

    AllPropertyVisibility = PublicProperty |
                            ProtectedProperty |
                            PrivateProperty,

    Property = AllPropertyVisibility |
               StaticProperty |
               InstanceProperty,


    AllPrivate = PrivateProperty | PrivateMethod,
    AllNonPrivate = All & ~AllPrivate
}

public class ClassMemberScannerVisitor : INewTypeInDiagramVisitor
{
    public static IReadOnlyList<PropertyUmlMember> ScanProperties(Type type, ReflectionFlags scanFlags)
    {
        IEnumerable<PropertyUmlMember> ScanProperties(ReflectionFlags additionalFlag, UmlMemberKind kind)
        {
            var flags = scanFlags & ReflectionFlags.AllPropertyVisibility;
            if (flags == 0)
                yield break;
            if (!scanFlags.HasFlag(additionalFlag)) yield break;
            var r = BindingFlags.Public | BindingFlags.NonPublic;
            if (additionalFlag == ReflectionFlags.InstanceProperty)
                r |= BindingFlags.Instance;
            else if (additionalFlag == ReflectionFlags.StaticProperty)
                r |= BindingFlags.Static;

            foreach (var pi in type.GetProperties(r))
            {
                //var a_canRead = pi.CanRead;
                //var a_canWrite = pi.CanWrite;
                var getterFlag = GetGetterFlag(pi.GetMethod);
                var setterFlag = GetSetterFlag(pi.SetMethod);
                if (H(flags, getterFlag) || H(flags, setterFlag))
                    yield return new PropertyUmlMember
                    {
                        Group      = 10,
                        Name       = pi.Name,
                        Property   = pi,
                        Kind       = kind,
                        Visibility = GetVisibilityFromFlags(getterFlag | setterFlag),
                    };
            }
        }

        var p1 = ScanProperties(ReflectionFlags.InstanceProperty, UmlMemberKind.Normal);
        var p2 = ScanProperties(ReflectionFlags.StaticProperty, UmlMemberKind.Static);
        return p1.Concat(p2).ToArray();
    }

    private static bool CheckSkipDefault(MethodInfo mi)
    {
        if (mi.IsSpecialName)
            return false;
        if (mi.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
            return false;
        return mi.DeclaringType != typeof(object);
    }

    private static ReflectionFlags GetGetterFlag(MethodInfo m)
    {
        return GetMFlag(m,
            ReflectionFlags.PublicGetterProperty,
            ReflectionFlags.ProtectedGetterProperty,
            ReflectionFlags.PrivateGetterProperty);
    }

    private static ReflectionFlags GetMFlag(MethodInfo? m, ReflectionFlags publicFlag,
        ReflectionFlags protectedFlag, ReflectionFlags privateFlag)
    {
        if (m == null)
            return 0;
        if (m.IsPrivate)
            return privateFlag;
        if (m.IsPublic)
            return publicFlag;
        return protectedFlag;
    }

    private static ReflectionFlags GetSetterFlag(MethodInfo m)
    {
        return GetMFlag(m,
            ReflectionFlags.PublicSetterProperty,
            ReflectionFlags.ProtectedSetterProperty,
            ReflectionFlags.PrivateSetterProperty);
    }

    private static VisibilityFlag GetVisibilityFromFlags(ReflectionFlags f)
    {
        if (H(f, ReflectionFlags.PublicProperty | ReflectionFlags.PublicMethod))
            return VisibilityFlag.Public;
        if (H(f, ReflectionFlags.ProtectedProperty | ReflectionFlags.ProtectedMethod))
            return VisibilityFlag.Protected;
        if (H(f, ReflectionFlags.PrivateProperty | ReflectionFlags.PrivateMethod))
            return VisibilityFlag.Protected;
        return VisibilityFlag.None;
    }

    private static bool H(ReflectionFlags a, ReflectionFlags b)
    {
        return (a & b) > 0;
    }

    public void Visit(UmlDiagram diagram, UmlEntity info)
    {
        var type = info.Type;

        var properties = ScanProperties(type, ScanFlags);
        info.Members.AddRange(properties);

        var r = BindingFlags.Public | BindingFlags.NonPublic;
        if (ScanFlags.HasFlag(ReflectionFlags.InstanceMethod))
            r |= BindingFlags.Instance;
        if (ScanFlags.HasFlag(ReflectionFlags.StaticMethod))
            r |= BindingFlags.Static;

        var methodInfos = type.GetMethods(r);
        {
            var h = SortAndPrepareMethods;
            if (h != null)
            {
                var args = new SortAndPrepareMethodsEventArgs {Methods = methodInfos};
                h.Invoke(this, args);
                methodInfos = args.Methods;
            }
        }
        foreach (var mi in methodInfos)
        {
            var add = CheckSkipDefault(mi);
            var flag = GetMFlag(mi, ReflectionFlags.PublicMethod, ReflectionFlags.ProtectedMethod,
                ReflectionFlags.PrivateMethod);
            if (add)
                if (!H(flag, ScanFlags))
                    add = false;

            var h = AddTypeToDiagram;
            if (h is null && !add)
                continue;

            var member = new MethodUmlMember
            {
                Group      = 20,
                Name       = mi.Name,
                Method     = mi,
                Visibility = GetVisibilityFromFlags(flag)
            };
            if (mi.IsAbstract)
                member.Kind = UmlMemberKind.Abstract;
            if (mi.IsStatic)
                member.Kind = UmlMemberKind.Static;

            if (h != null)
            {
                var args = new AddTypeToDiagramEventArgs
                {
                    Decision  = add ? AddDecision.Add : AddDecision.Skip,
                    Member    = mi,
                    UmlMember = member
                };
                h(this, args);
                if (args.Decision != AddDecision.Default)
                    add = args.Decision == AddDecision.Add;
            }

            if (!add)
                continue;

            info.Members.Add(member);
        }
    }

    public ReflectionFlags ScanFlags { get; set; } = ReflectionFlags.AllNonPrivate;

    public event EventHandler<AddTypeToDiagramEventArgs> AddTypeToDiagram;

    public event EventHandler<SortAndPrepareMethodsEventArgs> SortAndPrepareMethods;

    public sealed class AddTypeToDiagramEventArgs : EventArgs
    {
        public AddDecision     Decision  { get; set; }
        public MethodInfo      Member    { get; set; }
        public MethodUmlMember UmlMember { get; set; }
    }

    public sealed class SortAndPrepareMethodsEventArgs : EventArgs
    {
        public MethodInfo[] Methods { get; set; }
    }
}

public enum AddDecision
{
    Default,
    Add,
    Skip
}