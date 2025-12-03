namespace AutoUml;

public class UmlSkinParam
{
    public void WriteTo(CodeWriter code, string name)
    {
        code.Writeln("skinparam " + name + " {");
        code.IncIndent();
        Font?.WriteTo(code, "");
        ArrowFont?.WriteTo(code, "Arrow");
        AttributeFont?.WriteTo(code, "Attribute");
        StereotypeFont?.WriteTo(code, "Stereotype");

        code.Write("BackgroundColor", BackgroundColor);
        code.Write("BorderColor", BorderColor);
        code.DecIndent();
        code.Writeln("}");
    }

    public UmlSkinFont? Font           { get; set; }
    public UmlSkinFont? ArrowFont      { get; set; }
    public UmlSkinFont? StereotypeFont { get; set; }

    public UmlColor BorderColor     { get; set; }
    public UmlColor BackgroundColor { get; set; }
    public UmlSkinFont? AttributeFont   { get; set; }
}
