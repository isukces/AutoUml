using System;

namespace AutoUml
{
    public abstract class BaseRelationAttribute : Attribute, INoteProvider
    {
        protected BaseRelationAttribute(UmlRelationKind kind)
        {
            Kind = kind;
        }

        IUmlFill INoteProvider.GetNoteBackground()
        {
            return UmlColor.FromString(NoteBackground).ToFill();
        }


        string INoteProvider.GetNoteText()
        {
            return Note;
        }
        public string Name { get; set; }
        
        public string Note { get; set; }

        public string NoteBackground { get; set; }

        public UmlRelationKind Kind { get; }
        
        public UmlArrowDirections ArrowDirection { get; set; }
        
        public bool ForceAddToDiagram { get; set; }
        
        public Type RelatedType { get; set; }

    }
}