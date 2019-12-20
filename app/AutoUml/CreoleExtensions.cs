namespace AutoUml
{
    public static class CreoleExtensions
    {
        public static string CreoleBold(this string text)
        {
            return text.CreoleWrap("**");
        }

        public static string CreoleItalic(this string text)
        {
            return text.CreoleWrap("//");
        }

        public static string CreoleMonospaced(this string text)
        {
            return text.CreoleWrap("\"\"");
        }

        public static string CreoleStroked(this string text)
        {
            return text.CreoleWrap("--");
        }

        public static string CreoleUnderlined(this string text)
        {
            return text.CreoleWrap("__");
        }

        public static string CreoleWaved(this string text)
        {
            return text.CreoleWrap("~~");
        }
 
        
        public static string CreoleWrap(this string text, string prefixAndSuffix)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return prefixAndSuffix + text + prefixAndSuffix;
        }
        
 
        
             
        public static string CreoleWrapHtml(this string text, string tag, string arg=null)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var opening = string.IsNullOrEmpty(arg) ? tag : $"{tag}:{arg}";
            return $"<{opening}>{text}</{tag}>";
        }

        public static string CreoleFontSize(this string text, int size)
        {
            return CreoleWrapHtml(text, "size", size.ToInv());
        }

        public static string CreoleBackground(this string text, string color)
        {
            return CreoleWrapHtml(text, "back", color);
        }
        public static string CreoleFontColor(this string text, string color)
        {
            return CreoleWrapHtml(text, "color", color);
        }
    }
}