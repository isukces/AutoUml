using System.Collections.Generic;
using System.Reflection;

namespace AutoUml.Symbols;

public class SymbolLineUmlMember : UmlMember
{
    public void AddSymbol(PlantUmlText symbol)
    {
        Symbols.Add(symbol);
    }

    public override MemberInfo? GetMemberInfo()
    {
        return null;
    }

    public override void WriteTo(CodeWriter cf, UmlDiagram diagram)
    {
        var txt = string.Join(" ", Symbols);
        if (string.IsNullOrEmpty(txt))
            return;
        cf.Writeln(txt);
        cf.Writeln("==");
    }

    public List<PlantUmlText> Symbols { get; } = new List<PlantUmlText>();
}
