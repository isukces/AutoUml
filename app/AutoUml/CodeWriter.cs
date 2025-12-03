using System.Text;

namespace AutoUml;

public class CodeWriter
{
    public void Add(CodeWriter top)
    {
        _sb.AppendLine(top._sb.ToString());
    }

    public void Close()
    {
        DecIndent();
        Writeln("}");
    }

    public void DecIndent()
    {
        _indentCount--;
    }

    public void IncIndent()
    {
        _indentCount++;
    }

    public void Open(string txt)
    {
        Writeln(txt);
        Writeln("{");
        IncIndent();
    }

    public void OpenSameLine(string s)
    {
        Writeln(s.TrimEnd() + " {");
        IncIndent();
    }

    public void Writeln(string txt)
    {
        if (_indentCount > 0)
            _sb.AppendLine(new string(' ', _indentCount * 4) + txt);
        else
            _sb.AppendLine(txt);
    }

    public string Code => _sb.ToString();

    private readonly StringBuilder _sb = new();

    private int _indentCount;
}
