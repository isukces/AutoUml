using System;
using System.Reflection;

namespace AutoUml
{
    [Flags]
    public enum ReflectionFlags
    {
        Public = BindingFlags.Public,
        NonPublic = BindingFlags.NonPublic,
        Static = BindingFlags.Static,
        Instance = BindingFlags.Instance
    }

    public class ClassMemberScannerVisitor : INewTypeInDiagramVisitor
    {
        public void Visit(UmlDiagram diagram, UmlEntity info)
        {
            var type = info.Type;

            var flags = PropertiesBindingFlags & ~(ReflectionFlags.Instance | ReflectionFlags.Static);

            void Scan(ReflectionFlags additionalFlag, UmlMemberKind kind)
            {
                if (!PropertiesBindingFlags.HasFlag(additionalFlag)) return;
                foreach (var pi in type.GetProperties((BindingFlags)(flags | additionalFlag)))
                {
                    info.Members.Add(new PropertyUmlMember
                    {
                        Group    = 10,
                        Name     = pi.Name,
                        Property = pi,
                        Kind     = kind
                    });
                }
            }

            Scan(ReflectionFlags.Instance, UmlMemberKind.Normal);
            Scan(ReflectionFlags.Static, UmlMemberKind.Static);

            foreach (var mi in type.GetMethods((BindingFlags)MethodsBindingFlags))
            {
                if (mi.IsSpecialName)
                    continue;
                if (mi.DeclaringType == typeof(object))
                    continue;
                var member = new MethodUmlMember
                {
                    Group  = 20,
                    Name   = mi.Name,
                    Method = mi
                };
                if (mi.IsAbstract)
                    member.Kind = UmlMemberKind.Abstract;
                if (mi.IsStatic)
                    member.Kind = UmlMemberKind.Static;
                info.Members.Add(member);
            }
        }

        public ReflectionFlags PropertiesBindingFlags { get; set; } = ReflectionFlags.Instance | ReflectionFlags.Public;
        public ReflectionFlags MethodsBindingFlags    { get; set; } = ReflectionFlags.Instance | ReflectionFlags.Public;
    }
}