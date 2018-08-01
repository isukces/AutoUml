using System;

namespace AutoUml
{
    public class UmlRelationAttribute : Attribute, INoteWithLocationProvider
    {
        public UmlRelationAttribute(UmlRelationKind kind = UmlRelationKind.Aggregation,
            UmlArrowDirections arrowDirection = UmlArrowDirections.Auto)
        {
            Kind           = kind;
            ArrowDirection = arrowDirection;
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

        public UmlRelationKind Kind { get; }
        public UmlArrowDirections ArrowDirection { get; set; }
        public bool?  Multiple { get; set; }
        public bool ForceAddToDiagram { get; set; }

        public string Note { get; set; }
        public string NoteBackground { get; set; }
        public NoteLocation NoteLocation { get; set; }
    }
}