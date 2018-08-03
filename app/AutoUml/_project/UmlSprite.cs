using System;
using System.Text;

namespace AutoUml
{
    public class UmlSprite
    {
        public static string MakeCode(string spriteName)
        {
            return "<$" + spriteName + ">";
        }

        private static bool IsValidChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '-';
        }

        private static string RemoveWhiteSpaces(string data)
        {
            var sb = new StringBuilder();
            for (var index = 0; index < data.Length; index++)
            {
                var c = data[index];
                if (IsValidChar(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }

        public void Save(PlantUmlFile file, string spriteName)
        {
            var header = "sprite $" + spriteName + " " + SpriteFormat;
            var data1  = RemoveWhiteSpaces(Data);
            var w      = file.Top;
            if (data1.Length < 120)
            {
                w.Writeln(header + " " + data1);
                return;
            }

            w.Writeln(header + " {");
            var idx = 0;
            while (true)
            {
                var end = Math.Min(idx + 120, data1.Length);
                var len = end - idx;
                if (len < 1)
                    break;
                w.Writeln(data1.Substring(idx, len));
                idx = end;
            }

            w.Writeln("}");
        }

        private string GrayLevelToString(SpriteGrayLevels level)
        {
            switch (level)
            {
                case SpriteGrayLevels.Level4:
                    return "4";
                case SpriteGrayLevels.Level8:
                    return "8";
                case SpriteGrayLevels.Level16:
                    return "16";
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public int              Width     { get; set; }
        public int              Height    { get; set; }
        public SpriteGrayLevels GrayLevel { get; set; }
        public bool             Zipped    { get; set; }
        public string           Data      { get; set; }

        public string SpriteFormat
        {
            get
            {
                string zipped = Zipped ? "z" : "";
                var format = "[" + Width.ToInv() + "x" + Height.ToInv() + "/" +
                             GrayLevelToString(GrayLevel) + zipped + "]";
                return format;
            }
        }
    }

    public enum SpriteGrayLevels
    {
        Level4,
        Level8,
        Level16
    }
}
/*
  sprite $bug [15x15/16z] PKzR2i0m2BFMi15p__FEjQEqB1z27aeqCqixa8S4OT7C53cKpsHpaYPDJY_12MHM-BLRyywPhrrlw3qumqNThmXgd1TOterAZmOW8sgiJafogofWRwtV3nCF
 sprite $printer [15x15/8z] NOtH3W0W208HxFz_kMAhj7lHWpa1XC716sz0Pq4MVPEWfBHIuxP3L6kbTcizR8tAhzaqFvXwvFfPEqm0
 sprite $disk {
   444445566677881
   436000000009991
   43600000000ACA1
   53700000001A7A1
   53700000012B8A1
   53800000123B8A1
   63800001233C9A1
   634999AABBC99B1
   744566778899AB1
   7456AAAAA99AAB1
   8566AFC228AABB1
   8567AC8118BBBB1
   867BD4433BBBBB1
   39AAAAABBBBBBC1
}
 * 
 */