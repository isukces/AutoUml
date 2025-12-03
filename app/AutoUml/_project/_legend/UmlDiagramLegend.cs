using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUml;

public sealed class UmlDiagramLegend
{
    public void WriteTo(CodeWriter cf)
    {
        if (Items.Count == 0)
            return;
        var opening = GetOpening();
        cf.Writeln(opening);
        foreach (var legendItem in Items.OrderBy(a => a.SortOrder))
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

    public List<UmlDiagramLegendItem> Items { get; } = new List<UmlDiagramLegendItem>();

    public UmlDiagramLegendHorizontalAlignment HorizontalAlignment { get; set; }
    public UmlDiagramLegendVerticalAlignment   VerticalAlignment   { get; set; }
}