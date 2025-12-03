using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoUml.Symbols;

public class SymbolTableUmlDiagramLegendItem : UmlDiagramLegendItem
{
    public SymbolTableUmlDiagramLegendItem()
    {
        SortOrder = 50;
    }

    public void AddSymbol(SymbolInfo si)
    {
        if (si == null) throw new ArgumentNullException(nameof(si));
        Symbols[si.SymbolName] = si;
    }

    public override void WriteTo(CodeWriter cf)
    {
        if (Symbols.Count == 0)
            return;
        var text = Title?.Text;
        if (!string.IsNullOrEmpty(text))
            cf.Writeln(text);
        foreach (var i in Symbols.OrderBy(a => a.Key))
        {
            var value = i.Value;
            var code  = $"|  {value.SymbolText}  |  {value.Description}  |";
            cf.Writeln(code);
        }
    }

    public PlantUmlText Title { get; set; }

    public Dictionary<string, SymbolInfo> Symbols { get; } = new Dictionary<string, SymbolInfo>();
}
