namespace AutoUml;

public class UmlStartingLinesAttributeVisitor : NewTypeMultipleAttributeVisitor<UmlStartingLinesAttribute>
{
    protected override void VisitInternal(UmlDiagram diagram, UmlEntity info, UmlStartingLinesAttribute att)
    {
        var append = att.StartingLines;
        if (string.IsNullOrEmpty(append))
            return;
        switch (att.LineKind)
        {
            case ClassLineKind.Dot:
                append += "\n..";
                break;
            case ClassLineKind.Double:
                append += "\n==";
                break;
            case ClassLineKind.Single:
                append += "\n--";
                break;
            case ClassLineKind.SingleBold:
                append += "\n__";
                break;
        }

        info.AppendStartingLines(append);
    }
}