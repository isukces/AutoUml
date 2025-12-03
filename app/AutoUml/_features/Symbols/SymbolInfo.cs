using System;

namespace AutoUml.Symbols;

public class SymbolInfo
{
    public SymbolInfo(string symbolName, PlantUmlText symbolText, PlantUmlText description)
    {
        if (symbolText is null || symbolText.IsEmpty)
            throw new ArgumentException(nameof(symbolText));
        if (description is null || description.IsEmpty)
            throw new ArgumentException(nameof(description));
        SymbolName  = symbolName;
        SymbolText  = symbolText;
        Description = description;
    }

    public string       SymbolName  { get; set; }
    public PlantUmlText SymbolText  { get; set; }
    public PlantUmlText Description { get; set; }
}