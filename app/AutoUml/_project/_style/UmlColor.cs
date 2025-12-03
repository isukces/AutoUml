namespace AutoUml;

public struct UmlColor : IPlantUmlCodeProvider
{
    public UmlColor(string colorCode)
        : this(null, colorCode)
    {
    }

    private UmlColor(string? wellKnownName, string? color)
    {
        _isSet        = true;
        ColorCode     = color?.TrimStart('#', ' ').TrimEnd() ?? string.Empty;
        WellKnownName = wellKnownName;
    }


    public static UmlColor FromString(string? color)
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

    public static UmlColor Empty => new();

    public static UmlColor AliceBlue            => new("AliceBlue", "#F0F8FF");
    public static UmlColor AntiqueWhite         => new("AntiqueWhite", "#FAEBD7");
    public static UmlColor Aqua                 => new("Aqua", "#00FFFF");
    public static UmlColor Aquamarine           => new("Aquamarine", "#7FFFD4");
    public static UmlColor Azure                => new("Azure", "#F0FFFF");
    public static UmlColor Beige                => new("Beige", "#F5F5DC");
    public static UmlColor Bisque               => new("Bisque", "#FFE4C4");
    public static UmlColor Black                => new("Black", "#000000");
    public static UmlColor BlanchedAlmond       => new("BlanchedAlmond", "#FFEBCD");
    public static UmlColor Blue                 => new("Blue", "#0000FF");
    public static UmlColor BlueViolet           => new("BlueViolet", "#8A2BE2");
    public static UmlColor Brown                => new("Brown", "#A52A2A");
    public static UmlColor BurlyWood            => new("BurlyWood", "#DEB887");
    public static UmlColor CadetBlue            => new("CadetBlue", "#5F9EA0");
    public static UmlColor Chartreuse           => new("Chartreuse", "#7FFF00");
    public static UmlColor Chocolate            => new("Chocolate", "#D2691E");
    public static UmlColor Coral                => new("Coral", "#FF7F50");
    public static UmlColor CornflowerBlue       => new("CornflowerBlue", "#6495ED");
    public static UmlColor Cornsilk             => new("Cornsilk", "#FFF8DC");
    public static UmlColor Crimson              => new("Crimson", "#DC143C");
    public static UmlColor Cyan                 => new("Cyan", "#00FFFF");
    public static UmlColor DarkBlue             => new("DarkBlue", "#00008B");
    public static UmlColor DarkCyan             => new("DarkCyan", "#008B8B");
    public static UmlColor DarkGoldenRod        => new("DarkGoldenRod", "#B8860B");
    public static UmlColor DarkGray             => new("DarkGray", "#A9A9A9");
    public static UmlColor DarkGrey             => new("DarkGrey", "#A9A9A9");
    public static UmlColor DarkGreen            => new("DarkGreen", "#006400");
    public static UmlColor DarkKhaki            => new("DarkKhaki", "#BDB76B");
    public static UmlColor DarkMagenta          => new("DarkMagenta", "#8B008B");
    public static UmlColor DarkOliveGreen       => new("DarkOliveGreen", "#556B2F");
    public static UmlColor Darkorange           => new("Darkorange", "#FF8C00");
    public static UmlColor DarkOrchid           => new("DarkOrchid", "#9932CC");
    public static UmlColor DarkRed              => new("DarkRed", "#8B0000");
    public static UmlColor DarkSalmon           => new("DarkSalmon", "#E9967A");
    public static UmlColor DarkSeaGreen         => new("DarkSeaGreen", "#8FBC8F");
    public static UmlColor DarkSlateBlue        => new("DarkSlateBlue", "#483D8B");
    public static UmlColor DarkSlateGray        => new("DarkSlateGray", "#2F4F4F");
    public static UmlColor DarkSlateGrey        => new("DarkSlateGrey", "#2F4F4F");
    public static UmlColor DarkTurquoise        => new("DarkTurquoise", "#00CED1");
    public static UmlColor DarkViolet           => new("DarkViolet", "#9400D3");
    public static UmlColor DeepPink             => new("DeepPink", "#FF1493");
    public static UmlColor DeepSkyBlue          => new("DeepSkyBlue", "#00BFFF");
    public static UmlColor DimGray              => new("DimGray", "#696969");
    public static UmlColor DimGrey              => new("DimGrey", "#696969");
    public static UmlColor DodgerBlue           => new("DodgerBlue", "#1E90FF");
    public static UmlColor FireBrick            => new("FireBrick", "#B22222");
    public static UmlColor FloralWhite          => new("FloralWhite", "#FFFAF0");
    public static UmlColor ForestGreen          => new("ForestGreen", "#228B22");
    public static UmlColor Fuchsia              => new("Fuchsia", "#FF00FF");
    public static UmlColor Gainsboro            => new("Gainsboro", "#DCDCDC");
    public static UmlColor GhostWhite           => new("GhostWhite", "#F8F8FF");
    public static UmlColor Gold                 => new("Gold", "#FFD700");
    public static UmlColor GoldenRod            => new("GoldenRod", "#DAA520");
    public static UmlColor Gray                 => new("Gray", "#808080");
    public static UmlColor Grey                 => new("Grey", "#808080");
    public static UmlColor Green                => new("Green", "#008000");
    public static UmlColor GreenYellow          => new("GreenYellow", "#ADFF2F");
    public static UmlColor HoneyDew             => new("HoneyDew", "#F0FFF0");
    public static UmlColor HotPink              => new("HotPink", "#FF69B4");
    public static UmlColor IndianRed            => new("IndianRed", "#CD5C5C");
    public static UmlColor Indigo               => new("Indigo", "#4B0082");
    public static UmlColor Ivory                => new("Ivory", "#FFFFF0");
    public static UmlColor Khaki                => new("Khaki", "#F0E68C");
    public static UmlColor Lavender             => new("Lavender", "#E6E6FA");
    public static UmlColor LavenderBlush        => new("LavenderBlush", "#FFF0F5");
    public static UmlColor LawnGreen            => new("LawnGreen", "#7CFC00");
    public static UmlColor LemonChiffon         => new("LemonChiffon", "#FFFACD");
    public static UmlColor LightBlue            => new("LightBlue", "#ADD8E6");
    public static UmlColor LightCoral           => new("LightCoral", "#F08080");
    public static UmlColor LightCyan            => new("LightCyan", "#E0FFFF");
    public static UmlColor LightGoldenRodYellow => new("LightGoldenRodYellow", "#FAFAD2");
    public static UmlColor LightGray            => new("LightGray", "#D3D3D3");
    public static UmlColor LightGrey            => new("LightGrey", "#D3D3D3");
    public static UmlColor LightGreen           => new("LightGreen", "#90EE90");
    public static UmlColor LightPink            => new("LightPink", "#FFB6C1");
    public static UmlColor LightSalmon          => new("LightSalmon", "#FFA07A");
    public static UmlColor LightSeaGreen        => new("LightSeaGreen", "#20B2AA");
    public static UmlColor LightSkyBlue         => new("LightSkyBlue", "#87CEFA");
    public static UmlColor LightSlateGray       => new("LightSlateGray", "#778899");
    public static UmlColor LightSlateGrey       => new("LightSlateGrey", "#778899");
    public static UmlColor LightSteelBlue       => new("LightSteelBlue", "#B0C4DE");
    public static UmlColor LightYellow          => new("LightYellow", "#FFFFE0");
    public static UmlColor Lime                 => new("Lime", "#00FF00");
    public static UmlColor LimeGreen            => new("LimeGreen", "#32CD32");
    public static UmlColor Linen                => new("Linen", "#FAF0E6");
    public static UmlColor Magenta              => new("Magenta", "#FF00FF");
    public static UmlColor Maroon               => new("Maroon", "#800000");
    public static UmlColor MediumAquaMarine     => new("MediumAquaMarine", "#66CDAA");
    public static UmlColor MediumBlue           => new("MediumBlue", "#0000CD");
    public static UmlColor MediumOrchid         => new("MediumOrchid", "#BA55D3");
    public static UmlColor MediumPurple         => new("MediumPurple", "#9370D8");
    public static UmlColor MediumSeaGreen       => new("MediumSeaGreen", "#3CB371");
    public static UmlColor MediumSlateBlue      => new("MediumSlateBlue", "#7B68EE");
    public static UmlColor MediumSpringGreen    => new("MediumSpringGreen", "#00FA9A");
    public static UmlColor MediumTurquoise      => new("MediumTurquoise", "#48D1CC");
    public static UmlColor MediumVioletRed      => new("MediumVioletRed", "#C71585");
    public static UmlColor MidnightBlue         => new("MidnightBlue", "#191970");
    public static UmlColor MintCream            => new("MintCream", "#F5FFFA");
    public static UmlColor MistyRose            => new("MistyRose", "#FFE4E1");
    public static UmlColor Moccasin             => new("Moccasin", "#FFE4B5");
    public static UmlColor NavajoWhite          => new("NavajoWhite", "#FFDEAD");
    public static UmlColor Navy                 => new("Navy", "#000080");
    public static UmlColor OldLace              => new("OldLace", "#FDF5E6");
    public static UmlColor Olive                => new("Olive", "#808000");
    public static UmlColor OliveDrab            => new("OliveDrab", "#6B8E23");
    public static UmlColor Orange               => new("Orange", "#FFA500");
    public static UmlColor OrangeRed            => new("OrangeRed", "#FF4500");
    public static UmlColor Orchid               => new("Orchid", "#DA70D6");
    public static UmlColor PaleGoldenRod        => new("PaleGoldenRod", "#EEE8AA");
    public static UmlColor PaleGreen            => new("PaleGreen", "#98FB98");
    public static UmlColor PaleTurquoise        => new("PaleTurquoise", "#AFEEEE");
    public static UmlColor PaleVioletRed        => new("PaleVioletRed", "#D87093");
    public static UmlColor PapayaWhip           => new("PapayaWhip", "#FFEFD5");
    public static UmlColor PeachPuff            => new("PeachPuff", "#FFDAB9");
    public static UmlColor Peru                 => new("Peru", "#CD853F");
    public static UmlColor Pink                 => new("Pink", "#FFC0CB");
    public static UmlColor Plum                 => new("Plum", "#DDA0DD");
    public static UmlColor PowderBlue           => new("PowderBlue", "#B0E0E6");
    public static UmlColor Purple               => new("Purple", "#800080");
    public static UmlColor Red                  => new("Red", "#FF0000");
    public static UmlColor RosyBrown            => new("RosyBrown", "#BC8F8F");
    public static UmlColor RoyalBlue            => new("RoyalBlue", "#4169E1");
    public static UmlColor SaddleBrown          => new("SaddleBrown", "#8B4513");
    public static UmlColor Salmon               => new("Salmon", "#FA8072");
    public static UmlColor SandyBrown           => new("SandyBrown", "#F4A460");
    public static UmlColor SeaGreen             => new("SeaGreen", "#2E8B57");
    public static UmlColor SeaShell             => new("SeaShell", "#FFF5EE");
    public static UmlColor Sienna               => new("Sienna", "#A0522D");
    public static UmlColor Silver               => new("Silver", "#C0C0C0");
    public static UmlColor SkyBlue              => new("SkyBlue", "#87CEEB");
    public static UmlColor SlateBlue            => new("SlateBlue", "#6A5ACD");
    public static UmlColor SlateGray            => new("SlateGray", "#708090");
    public static UmlColor SlateGrey            => new("SlateGrey", "#708090");
    public static UmlColor Snow                 => new("Snow", "#FFFAFA");
    public static UmlColor SpringGreen          => new("SpringGreen", "#00FF7F");
    public static UmlColor SteelBlue            => new("SteelBlue", "#4682B4");
    public static UmlColor Tan                  => new("Tan", "#D2B48C");
    public static UmlColor Teal                 => new("Teal", "#008080");
    public static UmlColor Thistle              => new("Thistle", "#D8BFD8");
    public static UmlColor Tomato               => new("Tomato", "#FF6347");
    public static UmlColor Turquoise            => new("Turquoise", "#40E0D0");
    public static UmlColor Violet               => new("Violet", "#EE82EE");
    public static UmlColor Wheat                => new("Wheat", "#F5DEB3");
    public static UmlColor White                => new("White", "#FFFFFF");
    public static UmlColor WhiteSmoke           => new("WhiteSmoke", "#F5F5F5");
    public static UmlColor Yellow               => new("Yellow", "#FFFF00");
    public static UmlColor YellowGreen          => new("YellowGreen", "#9ACD32");

    /*
    public static UmlColor BUSINESS       => new UmlColor("BUSINESS", "#FFFF00");
    public static UmlColor APPLICATION    => new UmlColor("APPLICATION", "#A9DCDF");
    public static UmlColor MOTIVATION     => new UmlColor("MOTIVATION", "#B19CD9");
    public static UmlColor STRATEGY       => new UmlColor("STRATEGY", "#F6E4CC");
    public static UmlColor TECHNOLOGY     => new UmlColor("TECHNOLOGY", "#90EE90");
    public static UmlColor PHYSICAL       => new UmlColor("PHYSICAL", "#CCFFCC");
    public static UmlColor IMPLEMENTATION => new UmlColor("IMPLEMENTATION", "#FFA6BF");
    */

    public string? WellKnownName { get; set; }

    public string PlantUmlRgbCode => IsEmpty ? "" : "#" + ColorCode.ToLower();

    public string ColorCode { get; }

    public bool IsEmpty => !_isSet;

    public string PlantUmlCode => string.IsNullOrEmpty(WellKnownName)
        ? PlantUmlRgbCode
        : "#" + WellKnownName.ToLower();

    private readonly bool _isSet;
}
