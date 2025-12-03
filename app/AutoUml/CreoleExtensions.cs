namespace AutoUml;

public static class CreoleExtensions
{
    extension(string text)
    {
        public string CreoleBackground(string color)
        {
            return CreoleWrapHtml(text, "back", color);
        }

        public string CreoleBold()
        {
            return text.CreoleWrap("**");
        }

        public string CreoleFontColor(string color)
        {
            return CreoleWrapHtml(text, "color", color);
        }

        public string CreoleFontSize(int size)
        {
            return CreoleWrapHtml(text, "size", size.ToInv());
        }

        public string CreoleItalic()
        {
            return text.CreoleWrap("//");
        }

        public string CreoleMonospaced()
        {
            return text.CreoleWrap("\"\"");
        }

        public string CreoleStroked()
        {
            return text.CreoleWrap("--");
        }

        public string CreoleUnderlined()
        {
            return text.CreoleWrap("__");
        }

        public string CreoleWaved()
        {
            return text.CreoleWrap("~~");
        }

        public string CreoleWrap(string prefixAndSuffix)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return prefixAndSuffix + text + prefixAndSuffix;
        }

        public string CreoleWrapHtml(string tag, string? arg = null)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var opening = string.IsNullOrEmpty(arg) ? tag : $"{tag}:{arg}";
            return $"<{opening}>{text}</{tag}>";
        }
    }
}
