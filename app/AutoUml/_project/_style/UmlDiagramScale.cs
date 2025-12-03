namespace AutoUml;

public struct UmlDiagramScale
{
    public UmlDiagramScale(int width, int height)
    {
        Width  = width;
        Height = height;
        _isSet = true;
    }

    public override string ToString()
    {
        if (IsEmpty)
            return "";
        return Width.ToInv() + "*" + Height.ToInv();
    }

    public static UmlDiagramScale Max => new UmlDiagramScale(4096, 4096);

    public int Width  { get; }
    public int Height { get; }

    public bool IsEmpty => !_isSet;

    private readonly bool _isSet;
}

public enum ClassLineKind
{
    None,
    Dot,
    Single,
    SingleBold,
    Double
}