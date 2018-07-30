namespace AutoUml
{
    public class UmlCircleStyle
    {
        public void WriteTo(CodeWriter file)
        {
            Font?.WriteTo(file, "circledCharacter");
            file.Write("circledCharacterRadius", Radius);
        }

        public UmlSkinFont Font   { get; set; }
        public int?        Radius { get; set; }
    }
}