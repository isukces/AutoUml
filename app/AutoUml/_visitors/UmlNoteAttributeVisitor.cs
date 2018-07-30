using System.Reflection;

namespace AutoUml
{
    public class UmlNoteAttributeVisitor : INewTypeInDiagramVisitor
    {
        public void Visit(UmlProjectDiagram diagram, UmlEntity info)
        {
            var t = info.Type;
            foreach (var i in t.GetCustomAttributes<UmlNoteAttribute>())
                info.AddNote(i.NoteLocation, i.Text);
        }
    }
}