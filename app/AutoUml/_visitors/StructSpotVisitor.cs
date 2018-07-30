namespace AutoUml
{
    /// <summary>
    ///     Changes spot for .NET structs to 'S'
    /// </summary>
    public class StructSpotVisitor : INewTypeInDiagramVisitor
    {
        public StructSpotVisitor(UmlColor circleBackgroundColor)
        {
            CircleBackgroundColor = circleBackgroundColor.IsEmpty
                ? DefaultColor
                : circleBackgroundColor;
        }

        public void Visit(UmlProjectDiagram diagram, UmlEntity info)
        {
            var isStruct = info.Type.IsStruct();
            if (!isStruct) return;
            info.Spot                       = info.Spot ?? new UmlSpot();
            info.Spot.InCircle              = "S";
            info.Spot.CircleBackgroundColor = UmlColor.FromString("FF7700");
        }

        public UmlColor CircleBackgroundColor { get; set; }
        public static readonly UmlColor DefaultColor = UmlColor.FromString("FF7700");
    }
}