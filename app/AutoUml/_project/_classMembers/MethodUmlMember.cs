using System.Reflection;

namespace AutoUml;

public class MethodUmlMember : UmlMember
{
    public override MemberInfo? GetMemberInfo()
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
        code = GetCodePrefix() + code;
        var lines = code.MakeAction(MaxLineLength, (lineIndex, text) =>
        {
            if (lineIndex == 1)
                cf.IncIndent();
            cf.Writeln(text);
        });
        if (lines > 1)
            cf.DecIndent();
    }

    public static int MaxLineLength { get; set; } = 120;

    public required MethodInfo Method { get; set; }
}
