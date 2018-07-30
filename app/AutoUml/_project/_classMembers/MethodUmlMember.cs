using System.Reflection;

namespace AutoUml
{
    public class MethodUmlMember : UmlMember
    {
        public override string ToString()
        {
            return "m " + Method;
        }

        public override void WriteTo(CodeWriter cf, UmlProjectDiagram diagram)
        {
            var code = Method.MethodToUml(diagram.GetTypeName);
            cf.Writeln(code);
        }

        public MethodInfo Method { get; set; }
    }
}