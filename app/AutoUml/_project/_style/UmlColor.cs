namespace AutoUml;

public struct UmlColor : IPlantUmlCodeProvider
{
    public UmlColor(string colorCode)
        : this(null, colorCode)
    {
    }

    private UmlColor(string wellKnownName, string? color)
    {
        _isSet        = true;
        ColorCode     = color?.TrimStart('#', ' ').TrimEnd() ?? string.Empty;
        WellKnownName = wellKnownName;
    }


    public static UmlColor FromString(string color)
    {
        color = color?.Trim();
        return string.IsNullOrEmpty(color) ? Empty : new UmlColor(color);
    }

    public IUmlFill? ToFill()
    {
        return IsEmpty ? null : new SolidColorFill(this);
    }

    public override string ToString()
    {
        return PlantUmlCode;
    }

    public static UmlColor Empty => new UmlColor();

    public static UmlColor AliceBlue            => new UmlColor("AliceBlue", "#F0F8FF");
    public static UmlColor AntiqueWhite         => new UmlColor("AntiqueWhite", "#FAEBD7");
    public static UmlColor Aqua                 => new UmlColor("Aqua", "#00FFFF");
    public static UmlColor Aquamarine           => new UmlColor("Aquamarine", "#7FFFD4");
    public static UmlColor Azure                => new UmlColor("Azure", "#F0FFFF");
    public static UmlColor Beige                => new UmlColor("Beige", "#F5F5DC");
    public static UmlColor Bisque               => new UmlColor("Bisque", "#FFE4C4");
    public static UmlColor Black                => new UmlColor("Black", "#000000");
    public static UmlColor BlanchedAlmond       => new UmlColor("BlanchedAlmond", "#FFEBCD");
    public static UmlColor Blue                 => new UmlColor("Blue", "#0000FF");
    public static UmlColor BlueViolet           => new UmlColor("BlueViolet", "#8A2BE2");
    public static UmlColor Brown                => new UmlColor("Brown", "#A52A2A");
    public static UmlColor BurlyWood            => new UmlColor("BurlyWood", "#DEB887");
    public static UmlColor CadetBlue            => new UmlColor("CadetBlue", "#5F9EA0");
    public static UmlColor Chartreuse           => new UmlColor("Chartreuse", "#7FFF00");
    public static UmlColor Chocolate            => new UmlColor("Chocolate", "#D2691E");
    public static UmlColor Coral                => new UmlColor("Coral", "#FF7F50");
    public static UmlColor CornflowerBlue       => new UmlColor("CornflowerBlue", "#6495ED");
    public static UmlColor Cornsilk             => new UmlColor("Cornsilk", "#FFF8DC");
    public static UmlColor Crimson              => new UmlColor("Crimson", "#DC143C");
    public static UmlColor Cyan                 => new UmlColor("Cyan", "#00FFFF");
    public static UmlColor DarkBlue             => new UmlColor("DarkBlue", "#00008B");
    public static UmlColor DarkCyan             => new UmlColor("DarkCyan", "#008B8B");
    public static UmlColor DarkGoldenRod        => new UmlColor("DarkGoldenRod", "#B8860B");
    public static UmlColor DarkGray             => new UmlColor("DarkGray", "#A9A9A9");
    public static UmlColor DarkGrey             => new UmlColor("DarkGrey", "#A9A9A9");
    public static UmlColor DarkGreen            => new UmlColor("DarkGreen", "#006400");
    public static UmlColor DarkKhaki            => new UmlColor("DarkKhaki", "#BDB76B");
    public static UmlColor DarkMagenta          => new UmlColor("DarkMagenta", "#8B008B");
    public static UmlColor DarkOliveGreen       => new UmlColor("DarkOliveGreen", "#556B2F");
    public static UmlColor Darkorange           => new UmlColor("Darkorange", "#FF8C00");
    public static UmlColor DarkOrchid           => new UmlColor("DarkOrchid", "#9932CC");
    public static UmlColor DarkRed              => new UmlColor("DarkRed", "#8B0000");
    public static UmlColor DarkSalmon           => new UmlColor("DarkSalmon", "#E9967A");
    public static UmlColor DarkSeaGreen         => new UmlColor("DarkSeaGreen", "#8FBC8F");
    public static UmlColor DarkSlateBlue        => new UmlColor("DarkSlateBlue", "#483D8B");
    public static UmlColor DarkSlateGray        => new UmlColor("DarkSlateGray", "#2F4F4F");
    public static UmlColor DarkSlateGrey        => new UmlColor("DarkSlateGrey", "#2F4F4F");
    public static UmlColor DarkTurquoise        => new UmlColor("DarkTurquoise", "#00CED1");
    public static UmlColor DarkViolet           => new UmlColor("DarkViolet", "#9400D3");
    public static UmlColor DeepPink             => new UmlColor("DeepPink", "#FF1493");
    public static UmlColor DeepSkyBlue          => new UmlColor("DeepSkyBlue", "#00BFFF");
    public static UmlColor DimGray              => new UmlColor("DimGray", "#696969");
    public static UmlColor DimGrey              => new UmlColor("DimGrey", "#696969");
    public static UmlColor DodgerBlue           => new UmlColor("DodgerBlue", "#1E90FF");
    public static UmlColor FireBrick            => new UmlColor("FireBrick", "#B22222");
    public static UmlColor FloralWhite          => new UmlColor("FloralWhite", "#FFFAF0");
    public static UmlColor ForestGreen          => new UmlColor("ForestGreen", "#228B22");
    public static UmlColor Fuchsia              => new UmlColor("Fuchsia", "#FF00FF");
    public static UmlColor Gainsboro            => new UmlColor("Gainsboro", "#DCDCDC");
    public static UmlColor GhostWhite           => new UmlColor("GhostWhite", "#F8F8FF");
    public static UmlColor Gold                 => new UmlColor("Gold", "#FFD700");
    public static UmlColor GoldenRod            => new UmlColor("GoldenRod", "#DAA520");
    public static UmlColor Gray                 => new UmlColor("Gray", "#808080");
    public static UmlColor Grey                 => new UmlColor("Grey", "#808080");
    public static UmlColor Green                => new UmlColor("Green", "#008000");
    public static UmlColor GreenYellow          => new UmlColor("GreenYellow", "#ADFF2F");
    public static UmlColor HoneyDew             => new UmlColor("HoneyDew", "#F0FFF0");
    public static UmlColor HotPink              => new UmlColor("HotPink", "#FF69B4");
    public static UmlColor IndianRed            => new UmlColor("IndianRed", "#CD5C5C");
    public static UmlColor Indigo               => new UmlColor("Indigo", "#4B0082");
    public static UmlColor Ivory                => new UmlColor("Ivory", "#FFFFF0");
    public static UmlColor Khaki                => new UmlColor("Khaki", "#F0E68C");
    public static UmlColor Lavender             => new UmlColor("Lavender", "#E6E6FA");
    public static UmlColor LavenderBlush        => new UmlColor("LavenderBlush", "#FFF0F5");
    public static UmlColor LawnGreen            => new UmlColor("LawnGreen", "#7CFC00");
    public static UmlColor LemonChiffon         => new UmlColor("LemonChiffon", "#FFFACD");
    public static UmlColor LightBlue            => new UmlColor("LightBlue", "#ADD8E6");
    public static UmlColor LightCoral           => new UmlColor("LightCoral", "#F08080");
    public static UmlColor LightCyan            => new UmlColor("LightCyan", "#E0FFFF");
    public static UmlColor LightGoldenRodYellow => new UmlColor("LightGoldenRodYellow", "#FAFAD2");
    public static UmlColor LightGray            => new UmlColor("LightGray", "#D3D3D3");
    public static UmlColor LightGrey            => new UmlColor("LightGrey", "#D3D3D3");
    public static UmlColor LightGreen           => new UmlColor("LightGreen", "#90EE90");
    public static UmlColor LightPink            => new UmlColor("LightPink", "#FFB6C1");
    public static UmlColor LightSalmon          => new UmlColor("LightSalmon", "#FFA07A");
    public static UmlColor LightSeaGreen        => new UmlColor("LightSeaGreen", "#20B2AA");
    public static UmlColor LightSkyBlue         => new UmlColor("LightSkyBlue", "#87CEFA");
    public static UmlColor LightSlateGray       => new UmlColor("LightSlateGray", "#778899");
    public static UmlColor LightSlateGrey       => new UmlColor("LightSlateGrey", "#778899");
    public static UmlColor LightSteelBlue       => new UmlColor("LightSteelBlue", "#B0C4DE");
    public static UmlColor LightYellow          => new UmlColor("LightYellow", "#FFFFE0");
    public static UmlColor Lime                 => new UmlColor("Lime", "#00FF00");
    public static UmlColor LimeGreen            => new UmlColor("LimeGreen", "#32CD32");
    public static UmlColor Linen                => new UmlColor("Linen", "#FAF0E6");
    public static UmlColor Magenta              => new UmlColor("Magenta", "#FF00FF");
    public static UmlColor Maroon               => new UmlColor("Maroon", "#800000");
    public static UmlColor MediumAquaMarine     => new UmlColor("MediumAquaMarine", "#66CDAA");
    public static UmlColor MediumBlue           => new UmlColor("MediumBlue", "#0000CD");
    public static UmlColor MediumOrchid         => new UmlColor("MediumOrchid", "#BA55D3");
    public static UmlColor MediumPurple         => new UmlColor("MediumPurple", "#9370D8");
    public static UmlColor MediumSeaGreen       => new UmlColor("MediumSeaGreen", "#3CB371");
    public static UmlColor MediumSlateBlue      => new UmlColor("MediumSlateBlue", "#7B68EE");
    public static UmlColor MediumSpringGreen    => new UmlColor("MediumSpringGreen", "#00FA9A");
    public static UmlColor MediumTurquoise      => new UmlColor("MediumTurquoise", "#48D1CC");
    public static UmlColor MediumVioletRed      => new UmlColor("MediumVioletRed", "#C71585");
    public static UmlColor MidnightBlue         => new UmlColor("MidnightBlue", "#191970");
    public static UmlColor MintCream            => new UmlColor("MintCream", "#F5FFFA");
    public static UmlColor MistyRose            => new UmlColor("MistyRose", "#FFE4E1");
    public static UmlColor Moccasin             => new UmlColor("Moccasin", "#FFE4B5");
    public static UmlColor NavajoWhite          => new UmlColor("NavajoWhite", "#FFDEAD");
    public static UmlColor Navy                 => new UmlColor("Navy", "#000080");
    public static UmlColor OldLace              => new UmlColor("OldLace", "#FDF5E6");
    public static UmlColor Olive                => new UmlColor("Olive", "#808000");
    public static UmlColor OliveDrab            => new UmlColor("OliveDrab", "#6B8E23");
    public static UmlColor Orange               => new UmlColor("Orange", "#FFA500");
    public static UmlColor OrangeRed            => new UmlColor("OrangeRed", "#FF4500");
    public static UmlColor Orchid               => new UmlColor("Orchid", "#DA70D6");
    public static UmlColor PaleGoldenRod        => new UmlColor("PaleGoldenRod", "#EEE8AA");
    public static UmlColor PaleGreen            => new UmlColor("PaleGreen", "#98FB98");
    public static UmlColor PaleTurquoise        => new UmlColor("PaleTurquoise", "#AFEEEE");
    public static UmlColor PaleVioletRed        => new UmlColor("PaleVioletRed", "#D87093");
    public static UmlColor PapayaWhip           => new UmlColor("PapayaWhip", "#FFEFD5");
    public static UmlColor PeachPuff            => new UmlColor("PeachPuff", "#FFDAB9");
    public static UmlColor Peru                 => new UmlColor("Peru", "#CD853F");
    public static UmlColor Pink                 => new UmlColor("Pink", "#FFC0CB");
    public static UmlColor Plum                 => new UmlColor("Plum", "#DDA0DD");
    public static UmlColor PowderBlue           => new UmlColor("PowderBlue", "#B0E0E6");
    public static UmlColor Purple               => new UmlColor("Purple", "#800080");
    public static UmlColor Red                  => new UmlColor("Red", "#FF0000");
    public static UmlColor RosyBrown            => new UmlColor("RosyBrown", "#BC8F8F");
    public static UmlColor RoyalBlue            => new UmlColor("RoyalBlue", "#4169E1");
    public static UmlColor SaddleBrown          => new UmlColor("SaddleBrown", "#8B4513");
    public static UmlColor Salmon               => new UmlColor("Salmon", "#FA8072");
    public static UmlColor SandyBrown           => new UmlColor("SandyBrown", "#F4A460");
    public static UmlColor SeaGreen             => new UmlColor("SeaGreen", "#2E8B57");
    public static UmlColor SeaShell             => new UmlColor("SeaShell", "#FFF5EE");
    public static UmlColor Sienna               => new UmlColor("Sienna", "#A0522D");
    public static UmlColor Silver               => new UmlColor("Silver", "#C0C0C0");
    public static UmlColor SkyBlue              => new UmlColor("SkyBlue", "#87CEEB");
    public static UmlColor SlateBlue            => new UmlColor("SlateBlue", "#6A5ACD");
    public static UmlColor SlateGray            => new UmlColor("SlateGray", "#708090");
    public static UmlColor SlateGrey            => new UmlColor("SlateGrey", "#708090");
    public static UmlColor Snow                 => new UmlColor("Snow", "#FFFAFA");
    public static UmlColor SpringGreen          => new UmlColor("SpringGreen", "#00FF7F");
    public static UmlColor SteelBlue            => new UmlColor("SteelBlue", "#4682B4");
    public static UmlColor Tan                  => new UmlColor("Tan", "#D2B48C");
    public static UmlColor Teal                 => new UmlColor("Teal", "#008080");
    public static UmlColor Thistle              => new UmlColor("Thistle", "#D8BFD8");
    public static UmlColor Tomato               => new UmlColor("Tomato", "#FF6347");
    public static UmlColor Turquoise            => new UmlColor("Turquoise", "#40E0D0");
    public static UmlColor Violet               => new UmlColor("Violet", "#EE82EE");
    public static UmlColor Wheat                => new UmlColor("Wheat", "#F5DEB3");
    public static UmlColor White                => new UmlColor("White", "#FFFFFF");
    public static UmlColor WhiteSmoke           => new UmlColor("WhiteSmoke", "#F5F5F5");
    public static UmlColor Yellow               => new UmlColor("Yellow", "#FFFF00");
    public static UmlColor YellowGreen          => new UmlColor("YellowGreen", "#9ACD32");

    /*
    public static UmlColor BUSINESS       => new UmlColor("BUSINESS", "#FFFF00");
    public static UmlColor APPLICATION    => new UmlColor("APPLICATION", "#A9DCDF");
    public static UmlColor MOTIVATION     => new UmlColor("MOTIVATION", "#B19CD9");
    public static UmlColor STRATEGY       => new UmlColor("STRATEGY", "#F6E4CC");
    public static UmlColor TECHNOLOGY     => new UmlColor("TECHNOLOGY", "#90EE90");
    public static UmlColor PHYSICAL       => new UmlColor("PHYSICAL", "#CCFFCC");
    public static UmlColor IMPLEMENTATION => new UmlColor("IMPLEMENTATION", "#FFA6BF");
    */

    public string WellKnownName { get; set; }

    public string PlantUmlRgbCode => IsEmpty ? "" : "#" + ColorCode.ToLower();

    public string ColorCode { get; }

    public bool IsEmpty => !_isSet;

    public string PlantUmlCode => string.IsNullOrEmpty(WellKnownName)
        ? PlantUmlRgbCode
        : "#" + WellKnownName.ToLower();

    private readonly bool _isSet;
}
