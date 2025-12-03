using System.Diagnostics.CodeAnalysis;

namespace AutoUml.Symbols;

public sealed class SealedClassVisitor : SymbolBaseVisitor
{
    [SetsRequiredMembers]
    public SealedClassVisitor()
    {
        Symbol      = OpenIconicKind.Paperclip.AsPlantUmlText();
        SymbolColor = UmlColor.MediumBlue;
    }

    public override void Visit(UmlDiagram diagram, UmlEntity info)
    {
        if (!info.Type.IsSealed) return;
        var el = OpenIconicKind.Paperclip.AsPlantUmlText();
        el = AddStyle(el);
        var s = new SymbolInfo("sealed", el, "Sealed class");
        AddIcon(diagram, s);
        AddIcon(info, s);
    }
}
