using System;

namespace AutoUml
{
    /// <summary>
    ///     Use it when additional relation is necessary
    /// </summary>
    [AttributeUsage(AttributesConsts.Entities, AllowMultiple = true)]
    public class UmlAddRelationAttribute : Attribute, INoteProvider
    {
        public UmlAddRelationAttribute(Type relatedType, string name,
            UmlRelationKind kind = UmlRelationKind.Aggregation)
        {
            RelatedType = relatedType;
            Name        = name;
            Kind        = kind;
        }

        public UmlRelationKind Kind        { get; }
        public Type            RelatedType { get; }
        public string          Name        { get; }
        public string          Note        { get; set; }
        public string          NoteColor   { get; set; }
        public bool            Multiple    { get; set; }
        
        
        IUmlFill INoteProvider.GetNoteBackground()
        {
            return UmlColor.FromString(NoteColor).ToFill();
        }

        string INoteProvider.GetNoteText()
        {
            return Note;
        }
    }
}