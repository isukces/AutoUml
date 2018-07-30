using System;

namespace AutoUml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class UmlNoteAttribute : Attribute
    {
        public UmlNoteAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public NoteLocation NoteLocation { get; set; } = NoteLocation.Bottom;
    }
}