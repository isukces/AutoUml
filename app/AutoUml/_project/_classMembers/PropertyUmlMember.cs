using System.Reflection;

namespace AutoUml
{
    public class PropertyUmlMember : UmlMember
    {
        public override MemberInfo GetMemberInfo()
        {
            return Property;
        }

        public override string ToString()
        {
            return "p " + Property;
        }

        public override void WriteTo(CodeWriter cf, UmlProjectDiagram diagram)
        {
            var code = diagram.GetTypeName(Property.PropertyType) + " " + Property.Name;
            cf.Writeln(code);
        }

        public PropertyInfo Property { get; set; }
    }
}