namespace AutoUml;

public class UmlSkinParams
{
    public static UmlSkinParams WhiteHandWritten(int fontSize = 20)
    {
        return new UmlSkinParams
        {
            Handwritten = true,
            Class = new UmlSkinParam
            {
                Font = new UmlSkinFont
                {
                    Size = fontSize,
                    Name = HandWrittenFont
                },
                ArrowFont = new UmlSkinFont
                {
                    Size = fontSize,
                    Name = HandWrittenFont
                },
                AttributeFont = new UmlSkinFont
                {
                    Name = HandWrittenFont
                },
                StereotypeFont = new UmlSkinFont
                {
                    Size = fontSize,
                    Name = HandWrittenFont
                },
                BackgroundColor = UmlColor.White,
                BorderColor     = new UmlColor("000040")
            },
            NoteFont = new UmlSkinFont
            {
                //Size = fontSize,
                Name = HandWrittenFont
            }
        };
    }

    public void WriteTo(CodeWriter file)
    {
        const string sp = "skinparam ";

        if (Handwritten)
            file.Writeln(sp + "handwritten true");
        Circle?.WriteTo(file);
        Class?.WriteTo(file, "class");
        NoteFont?.WriteTo(file, sp + "note");
    }

    public bool           Handwritten { get; set; }
    public UmlSkinParam   Class       { get; set; }
    public UmlCircleStyle Circle      { get; set; }
    public UmlSkinFont    NoteFont    { get; set; }

    public static string HandWrittenFont = "Buxton Sketch";
}
