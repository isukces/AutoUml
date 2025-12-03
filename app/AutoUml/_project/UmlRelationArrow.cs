using System;
using System.Text;

namespace AutoUml;

public struct UmlRelationArrow
{
    public UmlRelationArrow(ArrowEnd leftSign, ArrowEnd rightSign, bool isDotted = false)
    {
        LeftSign       = leftSign;
        RightSign      = rightSign;
        IsDotted       = isDotted;
        ArrowDirection = UmlArrowDirections.Auto;
        Color          = UmlColor.Empty;
        ArrowLength    = 2;

        LeftSignDescription  = null;
        RightSignDescription = null;
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

    public static UmlRelationArrow MkArrow(BaseRelationAttribute att, bool isCollectionRelation)
    {
        var arrow = GetRelationByKind(att.Kind, isCollectionRelation)
            .WithAttribute(att);
        return arrow;
    }

    public override string ToString()
    {
        var lineSign   = IsDotted ? '.' : '-';
        var len        = Math.Max(2, ArrowLength);
        var leftLength = len / 2;
        var leftPart   = new string(lineSign, leftLength);
        var rightPart  = new string(lineSign, len - leftLength);
        var sugg = ArrowDirection == UmlArrowDirections.Auto
            ? ""
            : ArrowDirection.ToString().ToLower();
        if (!Color.IsEmpty)
            sugg = "[" + Color.PlantUmlCode + "]" + sugg;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(LeftSignDescription))
            sb.Append(LeftSignDescription.AddQuotes() + " ");
        sb.Append(LeftSignText + leftPart + sugg + rightPart + RightSignText);
        if (!string.IsNullOrEmpty(RightSignDescription))
            sb.Append(" " + RightSignDescription.AddQuotes());
        return sb.ToString();
    }

    public UmlRelationArrow With(UmlArrowDirections d)
    {
        ArrowDirection = d;
        return this;
    }

    public UmlRelationArrow WithAttribute(BaseRelationAttribute att)
    {
        if (att is null)
            return this;
        if (att.ArrowDirection != UmlArrowDirections.Auto)
            ArrowDirection = att.ArrowDirection;
        ArrowLength = Math.Max(2, att.ArrowLength);
        if (att.LeftSign != ForceArrowEnd.NotSet)
            LeftSign = (ArrowEnd)att.LeftSign;
        if (att.RightSign != ForceArrowEnd.NotSet)
            RightSign = (ArrowEnd)att.RightSign;
        if (att.IsDotted != ChangeDecision.Auto)
            IsDotted = att.IsDotted == ChangeDecision.Yes;

        if (!string.IsNullOrEmpty(att.LeftSignDescription))
            LeftSignDescription = att.LeftSignDescription;

        if (!string.IsNullOrEmpty(att.RightSignDescription))
            RightSignDescription = att.RightSignDescription;

        if (!string.IsNullOrEmpty(att.Color))
            Color = new UmlColor(att.Color);

        return this;
    }

    public static UmlRelationArrow AggregationLeft => new(ArrowEnd.DiamondWhite, ArrowEnd.ArrowOpen);

    public static UmlRelationArrow AggregationLeftMany => new(ArrowEnd.DiamondWhite, ArrowEnd.Multiple);

    public static UmlRelationArrow CompositionLeft => new(ArrowEnd.DiamondBlack, ArrowEnd.ArrowOpen);

    public static UmlRelationArrow CompositionLeftMany => new(ArrowEnd.DiamondBlack, ArrowEnd.Multiple);


    public static UmlRelationArrow InheritRight => new(ArrowEnd.Empty, ArrowEnd.ArrowWhite);


    public string? LeftSignDescription { get; set; }

    public string? RightSignDescription { get; set; }


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
    public UmlColor           Color          { get; set; }

    /// <summary>
    ///     Minimun 2 is used
    /// </summary>
    public int ArrowLength { get; set; }
}
