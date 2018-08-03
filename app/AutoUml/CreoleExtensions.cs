namespace AutoUml
{
    public static class CreoleExtensions
    {
        public static string CreoleBold(this string text) => text.CreoleWrap("**");
        public static string CreoleItalic(this string text) => text.CreoleWrap("//");
        public static string CreoleMonospaced(this string text) => text.CreoleWrap("\"\"");
        public static string CreoleStroked(this string text) => text.CreoleWrap("--");
        public static string CreoleUnderlined(this string text) => text.CreoleWrap("__");
        public static string CreoleWaved(this string text) => text.CreoleWrap("~~");

        public static string CreoleWrap(this string text, string prefixAndSuffix)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return prefixAndSuffix + text + prefixAndSuffix;
        }
    }
}