using System.Reflection;

namespace AutoUml
{
    public class UmlTextMember : UmlMember
    {
        public UmlTextMember(string text)
        {
            Text = text;
        }

        public override MemberInfo GetMemberInfo()
        {
            return null;
        }

        public override void WriteTo(CodeWriter cf, UmlProjectDiagram diagram)
        {
            cf.Writeln(Text);
        }

        public string Text { get; set; }
    }
}