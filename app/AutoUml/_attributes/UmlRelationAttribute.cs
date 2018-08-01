using System;

namespace AutoUml
{
    public class UmlRelationAttribute : Attribute, INoteProvider
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

        NoteLocation? INoteProvider.GetNoteLocation()
        {
            return NoteLocation;
        }

        string INoteProvider.GetNoteText()
        {
            return Note;
        }

        public UmlRelationKind Kind { get; }

        public UmlArrowDirections ArrowDirection { get; set; }

        public string Note     { get; set; }
        public bool?  Multiple { get; set; }


        public bool ForceAddToDiagram { get; set; }

        public string NoteBackground { get; set; }

        public NoteLocation NoteLocation { get; set; }
    }
}