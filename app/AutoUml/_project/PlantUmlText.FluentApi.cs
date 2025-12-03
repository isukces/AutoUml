namespace AutoUml;

public static class PlantUmlTextFluent
{
    public static PlantUmlText? WithBackground(this PlantUmlText src, string color)
    {
        return src.WithWrapHtml("back", color);
    }

    public static PlantUmlText? WithBold(this PlantUmlText src)
    {
        return src.WithWrap("**");
    }

    public static PlantUmlText WithFontColor(this PlantUmlText src, UmlColor color)
    {
        if (color.IsEmpty)
            return src;
        return src.WithWrapHtml("color", color.PlantUmlCode);
    }

    public static PlantUmlText? WithFontSize(this PlantUmlText src, int size)
    {
        return src.WithWrapHtml("size", size.ToInv());
    }

    public static PlantUmlText? WithItalic(this PlantUmlText src)
    {
        return src.WithWrap("//");
    }

    public static PlantUmlText? WithMonospaced(this PlantUmlText src)
    {
        return src.WithWrap("\"\"");
    }

    public static PlantUmlText WithTextInNewLine(this PlantUmlText src, string? x)
    {
        if (string.IsNullOrEmpty(x))
            return src;
        if (src is null || src.IsEmpty)
            return x;
        return $"{src.Text}\n{x}";
    }

    public static PlantUmlText? WithStroked(this PlantUmlText src)
    {
        return src.WithWrap("--");
    }

    public static PlantUmlText? WithUnderline(this PlantUmlText? src, string? color = null)
    {
        if (src is null || src.IsEmpty)
            return src;
        if (string.IsNullOrEmpty(color))
            return src.WithWrap("__");
        return src.WithWrapHtml("u", color);
    }

    public static PlantUmlText? WithWaved(this PlantUmlText src)
    {
        return src.WithWrap("~~");
    }

    public static PlantUmlText? WithWrap(this PlantUmlText? src, string prefixAndSuffix)
    {
        if (src is null || src.IsEmpty)
            return src;
        return prefixAndSuffix + src.Text + prefixAndSuffix;
    }

    public static PlantUmlText? WithWrapHtml(this PlantUmlText? src, string tag, string? arg = null)
    {
        if (src is null || src.IsEmpty)
            return src;
        var opening = string.IsNullOrEmpty(arg) ? tag : $"{tag}:{arg}";
        return $"<{opening}>{src.Text}</{tag}>";
    }
}