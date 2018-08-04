namespace AutoUml
{
    public class UmlNoteAttributeVisitor : NewTypeMultipleAttributeVisitor<UmlNoteAttribute>
    {
        protected override void VisitInternal(UmlDiagram diagram, UmlEntity info, UmlNoteAttribute att)
        {
            info.AddNote(att);
        }
    }
}