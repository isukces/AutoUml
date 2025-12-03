namespace AutoUml;

public struct UmlRelationEnd
{
    public UmlRelationEnd(string name, string? label = null)
    {
        Name  = name;
        Label = label;
    }

    public string? Left
    {
        get
        {
            if (string.IsNullOrEmpty(Label))
                return QuotedName;
            return QuotedName + " " + Label.AddQuotes();
        }
    }

    public string? Right
    {
        get
        {
            if (string.IsNullOrEmpty(Label))
                return QuotedName;
            return Label.AddQuotes() + " " + QuotedName;
        }
    }

    private string? QuotedName => Name.AddQuotesIfNecessary();

    public string Name  { get; }
    public string Label { get; }
}