namespace AutoUml
{
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

            var existing = info.StartingLines;

            if (string.IsNullOrEmpty(existing))
                info.StartingLines = append;
            else
                info.StartingLines = string.Format("{0}\n{1}", existing, append);
        }
    }
}