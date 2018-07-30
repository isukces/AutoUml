namespace AutoUml
{
    public struct UmlColor : IPlantUmlCodeProvider
    {
        public UmlColor(string color)
        {
            _isSet = true;
            Color  = color;
        }

        public static UmlColor FromString(string color)
        {
            color = color?.Trim();
            return string.IsNullOrEmpty(color) ? Empty : new UmlColor(color);
        }

        public override string ToString()
        {
            return PlantUmlCode;
        }

        public static UmlColor Empty
        {
            get { return new UmlColor(); }
        }

        public static UmlColor AliceBlue
        {
            get { return new UmlColor("AliceBlue"); }
        }

        public static UmlColor AntiqueWhite
        {
            get { return new UmlColor("AntiqueWhite"); }
        }

        public static UmlColor Aquamarine
        {
            get { return new UmlColor("Aquamarine"); }
        }

        public static UmlColor Aqua
        {
            get { return new UmlColor("Aqua"); }
        }

        public static UmlColor Azure
        {
            get { return new UmlColor("Azure"); }
        }

        public static UmlColor Beige
        {
            get { return new UmlColor("Beige"); }
        }

        public static UmlColor Bisque
        {
            get { return new UmlColor("Bisque"); }
        }

        public static UmlColor Black
        {
            get { return new UmlColor("Black"); }
        }

        public static UmlColor BlanchedAlmond
        {
            get { return new UmlColor("BlanchedAlmond"); }
        }

        public static UmlColor BlueViolet
        {
            get { return new UmlColor("BlueViolet"); }
        }

        public static UmlColor Blue
        {
            get { return new UmlColor("Blue"); }
        }

        public static UmlColor Brown
        {
            get { return new UmlColor("Brown"); }
        }

        public static UmlColor BurlyWood
        {
            get { return new UmlColor("BurlyWood"); }
        }

        public static UmlColor CadetBlue
        {
            get { return new UmlColor("CadetBlue"); }
        }

        public static UmlColor Chartreuse
        {
            get { return new UmlColor("Chartreuse"); }
        }

        public static UmlColor Chocolate
        {
            get { return new UmlColor("Chocolate"); }
        }

        public static UmlColor Coral
        {
            get { return new UmlColor("Coral"); }
        }

        public static UmlColor CornflowerBlue
        {
            get { return new UmlColor("CornflowerBlue"); }
        }

        public static UmlColor Cornsilk
        {
            get { return new UmlColor("Cornsilk"); }
        }

        public static UmlColor Crimson
        {
            get { return new UmlColor("Crimson"); }
        }

        public static UmlColor Cyan
        {
            get { return new UmlColor("Cyan"); }
        }

        public static UmlColor DarkBlue
        {
            get { return new UmlColor("DarkBlue"); }
        }

        public static UmlColor DarkCyan
        {
            get { return new UmlColor("DarkCyan"); }
        }

        public static UmlColor DarkGoldenRod
        {
            get { return new UmlColor("DarkGoldenRod"); }
        }

        public static UmlColor DarkGray
        {
            get { return new UmlColor("DarkGray"); }
        }

        public static UmlColor DarkGreen
        {
            get { return new UmlColor("DarkGreen"); }
        }

        public static UmlColor DarkKhaki
        {
            get { return new UmlColor("DarkKhaki"); }
        }

        public static UmlColor DarkMagenta
        {
            get { return new UmlColor("DarkMagenta"); }
        }

        public static UmlColor DarkOliveGreen
        {
            get { return new UmlColor("DarkOliveGreen"); }
        }

        public static UmlColor DarkOrchid
        {
            get { return new UmlColor("DarkOrchid"); }
        }

        public static UmlColor DarkRed
        {
            get { return new UmlColor("DarkRed"); }
        }

        public static UmlColor DarkSalmon
        {
            get { return new UmlColor("DarkSalmon"); }
        }

        public static UmlColor DarkSeaGreen
        {
            get { return new UmlColor("DarkSeaGreen"); }
        }

        public static UmlColor DarkSlateBlue
        {
            get { return new UmlColor("DarkSlateBlue"); }
        }

        public static UmlColor DarkSlateGray
        {
            get { return new UmlColor("DarkSlateGray"); }
        }

        public static UmlColor DarkTurquoise
        {
            get { return new UmlColor("DarkTurquoise"); }
        }

        public static UmlColor DarkViolet
        {
            get { return new UmlColor("DarkViolet"); }
        }

        public static UmlColor Darkorange
        {
            get { return new UmlColor("Darkorange"); }
        }

        public static UmlColor DeepPink
        {
            get { return new UmlColor("DeepPink"); }
        }

        public static UmlColor DeepSkyBlue
        {
            get { return new UmlColor("DeepSkyBlue"); }
        }

        public static UmlColor DimGray
        {
            get { return new UmlColor("DimGray"); }
        }

        public static UmlColor DodgerBlue
        {
            get { return new UmlColor("DodgerBlue"); }
        }

        public static UmlColor FireBrick
        {
            get { return new UmlColor("FireBrick"); }
        }

        public static UmlColor FloralWhite
        {
            get { return new UmlColor("FloralWhite"); }
        }

        public static UmlColor ForestGreen
        {
            get { return new UmlColor("ForestGreen"); }
        }

        public static UmlColor Fuchsia
        {
            get { return new UmlColor("Fuchsia"); }
        }

        public static UmlColor Gainsboro
        {
            get { return new UmlColor("Gainsboro"); }
        }

        public static UmlColor GhostWhite
        {
            get { return new UmlColor("GhostWhite"); }
        }

        public static UmlColor GoldenRod
        {
            get { return new UmlColor("GoldenRod"); }
        }

        public static UmlColor Gold
        {
            get { return new UmlColor("Gold"); }
        }

        public static UmlColor Gray
        {
            get { return new UmlColor("Gray"); }
        }

        public static UmlColor GreenYellow
        {
            get { return new UmlColor("GreenYellow"); }
        }

        public static UmlColor Green
        {
            get { return new UmlColor("Green"); }
        }

        public static UmlColor HoneyDew
        {
            get { return new UmlColor("HoneyDew"); }
        }

        public static UmlColor HotPink
        {
            get { return new UmlColor("HotPink"); }
        }

        public static UmlColor IndianRed
        {
            get { return new UmlColor("IndianRed"); }
        }

        public static UmlColor Indigo
        {
            get { return new UmlColor("Indigo"); }
        }

        public static UmlColor Ivory
        {
            get { return new UmlColor("Ivory"); }
        }

        public static UmlColor Khaki
        {
            get { return new UmlColor("Khaki"); }
        }

        public static UmlColor LavenderBlush
        {
            get { return new UmlColor("LavenderBlush"); }
        }

        public static UmlColor Lavender
        {
            get { return new UmlColor("Lavender"); }
        }

        public static UmlColor LawnGreen
        {
            get { return new UmlColor("LawnGreen"); }
        }

        public static UmlColor LemonChiffon
        {
            get { return new UmlColor("LemonChiffon"); }
        }

        public static UmlColor LightBlue
        {
            get { return new UmlColor("LightBlue"); }
        }

        public static UmlColor LightCoral
        {
            get { return new UmlColor("LightCoral"); }
        }

        public static UmlColor LightCyan
        {
            get { return new UmlColor("LightCyan"); }
        }

        public static UmlColor LightGoldenRodYellow
        {
            get { return new UmlColor("LightGoldenRodYellow"); }
        }

        public static UmlColor LightGreen
        {
            get { return new UmlColor("LightGreen"); }
        }

        public static UmlColor LightGrey
        {
            get { return new UmlColor("LightGrey"); }
        }

        public static UmlColor LightPink
        {
            get { return new UmlColor("LightPink"); }
        }

        public static UmlColor LightSalmon
        {
            get { return new UmlColor("LightSalmon"); }
        }

        public static UmlColor LightSeaGreen
        {
            get { return new UmlColor("LightSeaGreen"); }
        }

        public static UmlColor LightSkyBlue
        {
            get { return new UmlColor("LightSkyBlue"); }
        }

        public static UmlColor LightSlateGray
        {
            get { return new UmlColor("LightSlateGray"); }
        }

        public static UmlColor LightSteelBlue
        {
            get { return new UmlColor("LightSteelBlue"); }
        }

        public static UmlColor LightYellow
        {
            get { return new UmlColor("LightYellow"); }
        }

        public static UmlColor LimeGreen
        {
            get { return new UmlColor("LimeGreen"); }
        }

        public static UmlColor Lime
        {
            get { return new UmlColor("Lime"); }
        }

        public static UmlColor Linen
        {
            get { return new UmlColor("Linen"); }
        }

        public static UmlColor Magenta
        {
            get { return new UmlColor("Magenta"); }
        }

        public static UmlColor Maroon
        {
            get { return new UmlColor("Maroon"); }
        }

        public static UmlColor MediumAquaMarine
        {
            get { return new UmlColor("MediumAquaMarine"); }
        }

        public static UmlColor MediumBlue
        {
            get { return new UmlColor("MediumBlue"); }
        }

        public static UmlColor MediumOrchid
        {
            get { return new UmlColor("MediumOrchid"); }
        }

        public static UmlColor MediumPurple
        {
            get { return new UmlColor("MediumPurple"); }
        }

        public static UmlColor MediumSeaGreen
        {
            get { return new UmlColor("MediumSeaGreen"); }
        }

        public static UmlColor MediumSlateBlue
        {
            get { return new UmlColor("MediumSlateBlue"); }
        }

        public static UmlColor MediumSpringGreen
        {
            get { return new UmlColor("MediumSpringGreen"); }
        }

        public static UmlColor MediumTurquoise
        {
            get { return new UmlColor("MediumTurquoise"); }
        }

        public static UmlColor MediumVioletRed
        {
            get { return new UmlColor("MediumVioletRed"); }
        }

        public static UmlColor MidnightBlue
        {
            get { return new UmlColor("MidnightBlue"); }
        }

        public static UmlColor MintCream
        {
            get { return new UmlColor("MintCream"); }
        }

        public static UmlColor MistyRose
        {
            get { return new UmlColor("MistyRose"); }
        }

        public static UmlColor Moccasin
        {
            get { return new UmlColor("Moccasin"); }
        }

        public static UmlColor NavajoWhite
        {
            get { return new UmlColor("NavajoWhite"); }
        }

        public static UmlColor Navy
        {
            get { return new UmlColor("Navy"); }
        }

        public static UmlColor OldLace
        {
            get { return new UmlColor("OldLace"); }
        }

        public static UmlColor OliveDrab
        {
            get { return new UmlColor("OliveDrab"); }
        }

        public static UmlColor Olive
        {
            get { return new UmlColor("Olive"); }
        }

        public static UmlColor OrangeRed
        {
            get { return new UmlColor("OrangeRed"); }
        }

        public static UmlColor Orange
        {
            get { return new UmlColor("Orange"); }
        }

        public static UmlColor Orchid
        {
            get { return new UmlColor("Orchid"); }
        }

        public static UmlColor PaleGoldenRod
        {
            get { return new UmlColor("PaleGoldenRod"); }
        }

        public static UmlColor PaleGreen
        {
            get { return new UmlColor("PaleGreen"); }
        }

        public static UmlColor PaleTurquoise
        {
            get { return new UmlColor("PaleTurquoise"); }
        }

        public static UmlColor PaleVioletRed
        {
            get { return new UmlColor("PaleVioletRed"); }
        }

        public static UmlColor PapayaWhip
        {
            get { return new UmlColor("PapayaWhip"); }
        }

        public static UmlColor PeachPuff
        {
            get { return new UmlColor("PeachPuff"); }
        }

        public static UmlColor Peru
        {
            get { return new UmlColor("Peru"); }
        }

        public static UmlColor Pink
        {
            get { return new UmlColor("Pink"); }
        }

        public static UmlColor Plum
        {
            get { return new UmlColor("Plum"); }
        }

        public static UmlColor PowderBlue
        {
            get { return new UmlColor("PowderBlue"); }
        }

        public static UmlColor Purple
        {
            get { return new UmlColor("Purple"); }
        }

        public static UmlColor Red
        {
            get { return new UmlColor("Red"); }
        }

        public static UmlColor RosyBrown
        {
            get { return new UmlColor("RosyBrown"); }
        }

        public static UmlColor RoyalBlue
        {
            get { return new UmlColor("RoyalBlue"); }
        }

        public static UmlColor SaddleBrown
        {
            get { return new UmlColor("SaddleBrown"); }
        }

        public static UmlColor Salmon
        {
            get { return new UmlColor("Salmon"); }
        }

        public static UmlColor SandyBrown
        {
            get { return new UmlColor("SandyBrown"); }
        }

        public static UmlColor SeaGreen
        {
            get { return new UmlColor("SeaGreen"); }
        }

        public static UmlColor SeaShell
        {
            get { return new UmlColor("SeaShell"); }
        }

        public static UmlColor Sienna
        {
            get { return new UmlColor("Sienna"); }
        }

        public static UmlColor Silver
        {
            get { return new UmlColor("Silver"); }
        }

        public static UmlColor SkyBlue
        {
            get { return new UmlColor("SkyBlue"); }
        }

        public static UmlColor SlateBlue
        {
            get { return new UmlColor("SlateBlue"); }
        }

        public static UmlColor SlateGray
        {
            get { return new UmlColor("SlateGray"); }
        }

        public static UmlColor Snow
        {
            get { return new UmlColor("Snow"); }
        }

        public static UmlColor SpringGreen
        {
            get { return new UmlColor("SpringGreen"); }
        }

        public static UmlColor SteelBlue
        {
            get { return new UmlColor("SteelBlue"); }
        }

        public static UmlColor Tan
        {
            get { return new UmlColor("Tan"); }
        }

        public static UmlColor Teal
        {
            get { return new UmlColor("Teal"); }
        }

        public static UmlColor Thistle
        {
            get { return new UmlColor("Thistle"); }
        }

        public static UmlColor Tomato
        {
            get { return new UmlColor("Tomato"); }
        }

        public static UmlColor Turquoise
        {
            get { return new UmlColor("Turquoise"); }
        }

        public static UmlColor Violet
        {
            get { return new UmlColor("Violet"); }
        }

        public static UmlColor Wheat
        {
            get { return new UmlColor("Wheat"); }
        }

        public static UmlColor WhiteSmoke
        {
            get { return new UmlColor("WhiteSmoke"); }
        }

        public static UmlColor White
        {
            get { return new UmlColor("White"); }
        }

        public static UmlColor YellowGreen
        {
            get { return new UmlColor("YellowGreen"); }
        }

        public static UmlColor Yellow
        {
            get { return new UmlColor("Yellow"); }
        }

        public string PlantUmlCode
        {
            get
            {
                if (IsEmpty)
                    return "";
                return "#" + Color;
            }
        }

        public string Color { get; }

        public bool IsEmpty
        {
            get { return !_isSet; }
        }

        private readonly bool _isSet;
    }
}