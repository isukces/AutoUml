using System;

namespace AutoUml
{
    [AttributeUsage(AttributesConsts.Entities)]
    public class UmlNoteAttribute : Attribute, INoteProvider
    {
        public UmlNoteAttribute(string note)
        {
            Note = note;
        }

        IUmlFill INoteProvider.GetNoteBackground()
        {
            return UmlColor.FromString(NoteBackground).ToFill();
        }

        NoteLocation INoteProvider.GetNoteLocation()
        {
            return NoteLocation;
        }

        string INoteProvider.GetNoteText()
        {
            return Note;
        }

        public string Note { get; set; }

        public NoteLocation NoteLocation { get; set; } = NoteLocation.Bottom;

        public string NoteBackground { get; set; }
    }
}