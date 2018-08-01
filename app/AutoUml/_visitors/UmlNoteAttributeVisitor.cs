using System.Reflection;

namespace AutoUml
{
    public class UmlNoteAttributeVisitor : NewTypeMultipleAttributeVisitor<UmlNoteAttribute>
    {
        protected override void VisitInternal(UmlProjectDiagram diagram, UmlEntity info, UmlNoteAttribute att)
        {
            info.AddNote(att);            
        }
    }
}