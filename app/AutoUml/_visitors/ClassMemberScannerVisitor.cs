using System.Reflection;

namespace AutoUml
{
    public class ClassMemberScannerVisitor : INewTypeInDiagramVisitor
    {
        public void Visit(UmlProjectDiagram diagram, UmlEntity info)
        {
            var t = info.Type;

            foreach (var pi in t.GetPropertiesInstancePublic())
            {
                info.Members.Add(new PropertyUmlMember
                {
                    Group    = 10,
                    Name     = pi.Name,
                    Property = pi
                });
            }

            foreach (var mi in t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                if (mi.IsSpecialName)
                    continue;
                if (mi.DeclaringType == typeof(object))
                    continue;
                info.Members.Add(new MethodUmlMember
                {
                    Group  = 20,
                    Name   = mi.Name,
                    Method = mi
                });
            }
        }
    }
}