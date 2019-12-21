namespace AutoUml
{
    public class TextUmlDiagramLegendItem : UmlDiagramLegendItem
    {
        public override void WriteTo(CodeWriter cf)
        {
            if (Text.IsEmpty)
                return;
            var lines = Text.SplitLines(true);
            foreach (var line in lines)
                cf.Writeln(line);
        }

        public PlantUmlText Text { get; set; }
    }
}