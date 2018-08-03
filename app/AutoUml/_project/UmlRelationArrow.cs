using System;

namespace AutoUml
{
    public struct UmlRelationArrow
    {
        public UmlRelationArrow(ArrowEnd leftSign, ArrowEnd rightSign, bool isDotted = false)
        {
            LeftSign       = leftSign;
            RightSign      = rightSign;
            IsDotted       = isDotted;
            ArrowDirection = UmlArrowDirections.Auto;
            Color          = UmlColor.Empty;
        }

        public static UmlRelationArrow GetRelationByKind(UmlRelationKind attKind, bool multiple)
        {
            UmlRelationArrow result;
            switch (attKind)
            {
                case UmlRelationKind.Aggregation:
                    result = new UmlRelationArrow(ArrowEnd.DiamondWhite, ArrowEnd.Empty);
                    break;
                case UmlRelationKind.AggregationDotted:
                    result = new UmlRelationArrow(ArrowEnd.DiamondWhite, ArrowEnd.Empty, true);
                    break;
                case UmlRelationKind.Composition:
                    result = new UmlRelationArrow(ArrowEnd.DiamondBlack, ArrowEnd.Empty);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(attKind), attKind, null);
            }

            result.RightSign = multiple ? ArrowEnd.Multiple : ArrowEnd.ArrowOpen;
            return result;
        }

        public override string ToString()
        {
            var line = IsDotted ? "." : "-";
            var sugg = ArrowDirection == UmlArrowDirections.Auto
                ? ""
                : ArrowDirection.ToString().ToLower();
            if (!Color.IsEmpty)
                sugg = "[" + Color.PlantUmlCode + "]" + sugg;
            return LeftSignText + line + sugg + line + RightSignText;
        }

        public UmlRelationArrow With(UmlArrowDirections d)
        {
            ArrowDirection = d;
            return this;
        }


        public static UmlRelationArrow AggregationLeft => new UmlRelationArrow(ArrowEnd.DiamondWhite, ArrowEnd.ArrowOpen);

        public static UmlRelationArrow AggregationLeftMany => new UmlRelationArrow(ArrowEnd.DiamondWhite, ArrowEnd.Multiple);

        public static UmlRelationArrow CompositionLeft => new UmlRelationArrow(ArrowEnd.DiamondBlack, ArrowEnd.ArrowOpen);

        public static UmlRelationArrow CompositionLeftMany => new UmlRelationArrow(ArrowEnd.DiamondBlack, ArrowEnd.Multiple);


        public static UmlRelationArrow InheritRight => new UmlRelationArrow(ArrowEnd.Empty, ArrowEnd.ArrowWhite);

        public UmlColor Color { get; set; }

        private string LeftSignText
        {
            get
            {
                switch (LeftSign)
                {
                    case ArrowEnd.Empty: return "";
                    case ArrowEnd.DiamondWhite:
                        return "o";
                    case ArrowEnd.DiamondBlack:
                        return "*";
                    case ArrowEnd.ArrowOpen:
                        return "<";
                    case ArrowEnd.ArrowWhite:
                        return "<|";
                    case ArrowEnd.Multiple:
                        return "}";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private string RightSignText
        {
            get
            {
                switch (RightSign)
                {
                    case ArrowEnd.Empty: return "";
                    case ArrowEnd.DiamondWhite:
                        return "o";
                    case ArrowEnd.DiamondBlack:
                        return "*";
                    case ArrowEnd.ArrowOpen:
                        return ">";
                    case ArrowEnd.ArrowWhite:
                        return "|>";
                    case ArrowEnd.Multiple:
                        return "{";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public UmlArrowDirections ArrowDirection { get; set; }
        public bool               IsDotted       { get; set; }
        public ArrowEnd           LeftSign       { get; set; }
        public ArrowEnd           RightSign      { get; set; }
    }
}