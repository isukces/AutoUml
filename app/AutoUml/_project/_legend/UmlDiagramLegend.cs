using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace AutoUml
{
    public sealed class UmlDiagramLegend
    {
        public void WriteTo(CodeWriter cf)
        {
            if (Items.Count == 0)
                return;
            var opening = GetOpening();
            cf.Writeln(opening);
            foreach (var legendItem in Items)
                legendItem.WriteTo(cf);
            cf.Writeln("endlegend");
        }

        private string GetOpening()
        {
            var q = new StringBuilder();
            q.Append("legend");
            if (VerticalAlignment != UmlDiagramLegendVerticalAlignment.Auto)
            {
                q.Append(" ");
                q.Append(VerticalAlignment.ToString().ToLower());
            }

            if (HorizontalAlignment != UmlDiagramLegendHorizontalAlignment.Auto)
            {
                q.Append(" ");
                q.Append(HorizontalAlignment.ToString().ToLower());
            }

            return q.ToString();
        }

        [NotNull]
        public List<UmlDiagramLegendItem> Items { get; } = new List<UmlDiagramLegendItem>();

        public UmlDiagramLegendHorizontalAlignment HorizontalAlignment { get; set; }
        public UmlDiagramLegendVerticalAlignment   VerticalAlignment   { get; set; }
    }

    public abstract class UmlDiagramLegendItem
    {
        public abstract void WriteTo(CodeWriter cf);
        public string Tag       { get; set; }
        public int    SortOrder { get; set; }
    }

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

    public enum UmlDiagramLegendHorizontalAlignment
    {
        Auto,
        Left,
        Center,
        Right
    }

    public enum UmlDiagramLegendVerticalAlignment
    {
        Auto,
        Top,
        Bottom
    }
}