namespace AutoUml;

public abstract class UmlDiagramLegendItem
{
    public abstract void WriteTo(CodeWriter cf);

    public string? Tag       { get; set; }
    public int     SortOrder { get; set; }
}
