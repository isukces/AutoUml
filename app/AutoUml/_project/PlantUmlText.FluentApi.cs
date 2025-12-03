namespace AutoUml;

public static class PlantUmlTextFluent
{
    extension(PlantUmlText src)
    {
        public PlantUmlText? WithBackground(string color)
        {
            return src.WithWrapHtml("back", color);
        }

        public PlantUmlText? WithBold()
        {
            return src.WithWrap("**");
        }

        public PlantUmlText WithFontColor(UmlColor color)
        {
            if (color.IsEmpty)
                return src;
            return src.WithWrapHtml("color", color.PlantUmlCode) ?? "";
        }

        public PlantUmlText WithFontSize(int size)
        {
            return src.WithWrapHtml("size", size.ToInv())!;
        }

        public PlantUmlText? WithItalic()
        {
            return src.WithWrap("//");
        }

        public PlantUmlText? WithMonospaced()
        {
            return src.WithWrap("\"\"");
        }

        public PlantUmlText? WithStroked()
        {
            return src.WithWrap("--");
        }
    }

    extension(PlantUmlText? src)
    {
        public PlantUmlText WithTextInNewLine(string? x)
        {
            if (string.IsNullOrEmpty(x))
                return src ?? "";
            if (src is null || src.IsEmpty)
                return x;
            return $"{src.Text}\n{x}";
        }

        public PlantUmlText? WithUnderline(string? color = null)
        {
            if (src is null || src.IsEmpty)
                return src;
            if (string.IsNullOrEmpty(color))
                return src.WithWrap("__");
            return src.WithWrapHtml("u", color);
        }
    }

    extension(PlantUmlText src)
    {
        public PlantUmlText? WithWaved()
        {
            return src.WithWrap("~~");
        }
    }

    extension(PlantUmlText? src)
    {
        public PlantUmlText? WithWrap(string prefixAndSuffix)
        {
            if (src is null || src.IsEmpty)
                return src;
            return prefixAndSuffix + src.Text + prefixAndSuffix;
        }

        public PlantUmlText? WithWrapHtml(string tag, string? arg = null)
        {
            if (src is null || src.IsEmpty)
                return src;
            var opening = string.IsNullOrEmpty(arg) ? tag : $"{tag}:{arg}";
            return $"<{opening}>{src.Text}</{tag}>";
        }
    }
}
