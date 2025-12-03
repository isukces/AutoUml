namespace AutoUml;

public class UmlSkinFont
{
    public void WriteTo(CodeWriter file, string prefix)
    {
        file.Write(prefix + "FontSize", Size);
        file.Write(prefix + "FontName", Name);
        file.Write("FontColor", Color);
    }

    public int?     Size  { get; set; }
    public string   Name  { get; set; }
    public UmlColor Color { get; set; }
}