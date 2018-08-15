using System.Reflection;

namespace AutoUml
{
    public class MethodUmlMember : UmlMember
    {
        public override MemberInfo GetMemberInfo()
        {
            return Method;
        }

        public override string ToString()
        {
            return "m " + Method;
        }

        public override void WriteTo(CodeWriter cf, UmlDiagram diagram)
        {
            var code = Method.MethodToUml(diagram.GetTypeName);          
            cf.Writeln(GetCodePrefix()+code);
        }
        

        public MethodInfo Method { get; set; }
    }
}