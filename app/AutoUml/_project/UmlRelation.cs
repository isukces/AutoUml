namespace AutoUml
{
    public class UmlRelation
    {
        public override string ToString()
        {
            var a = Left.Left + " " + Arrow + " " + Right.Right;
            if (string.IsNullOrEmpty(Label))
                return a;
            return a + ":" + Label.AddQuotes();
        }

        public UmlRelation With(UmlArrowDirections dir)
        {
            Arrow = Arrow.With(dir);
            return this;
        }

        public UmlRelationEnd Left  { get; set; }
        public UmlRelationEnd Right { get; set; }

        public UmlRelationArrow Arrow     { get; set; }
        public string           Label     { get; set; }
        public string           Note      { get; set; }
        public string           NoteColor { get; set; }
    }
}