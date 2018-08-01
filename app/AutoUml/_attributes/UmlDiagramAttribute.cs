using System;

namespace AutoUml
{
    [AttributeUsage(AttributesConsts.Entities, AllowMultiple = true)]
    public class UmlDiagramAttribute : Attribute, IEntityBackgroundProvider, INoteProvider
    {
        public UmlDiagramAttribute(string diagramName)
        {
            DiagramName = diagramName;
        }

        IUmlFill IEntityBackgroundProvider.GetEntityBackground()
        {
            return UmlColor.FromString(BackgroundColor).ToFill();
        }

        IUmlFill INoteProvider.GetNoteBackground()
        {
            return UmlColor.FromString(NoteBackground).ToFill();
        }

        NoteLocation? INoteProvider.GetNoteLocation()
        {
            return NoteLocation;
        }

        string INoteProvider.GetNoteText()
        {
            return Note;
        }

        public string DiagramName { get; }

        public string BackgroundColor { get; set; }

        public string Note { get; set; }

        public NoteLocation NoteLocation { get; set; }

        public string NoteBackground { get; set; }
    }
}