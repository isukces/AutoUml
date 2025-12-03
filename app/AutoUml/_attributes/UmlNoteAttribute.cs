using System;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities, AllowMultiple = true)]
public class UmlNoteAttribute : Attribute, INoteWithLocationProvider
{
    public UmlNoteAttribute(string note)
    {
        Note = note;
    }

    IUmlFill INoteProvider.GetNoteBackground()
    {
        return UmlColor.FromString(NoteBackground).ToFill();
    }

    NoteLocation INoteWithLocationProvider.GetNoteLocation()
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
