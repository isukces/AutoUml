using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public class UmlRelation : IMetadataContainer
    {
        public UmlRelation WitCreatorMeta<T>(Type leftType, Type rightType)
        {
            Metadata["creator"]   = typeof(T);
            Metadata["leftType"]  = leftType;
            Metadata["rightType"] = rightType;
            return this;
        }

        public override string ToString()
        {
            var a = Left.Left + " " + Arrow + " " + Right.Right;
            if (string.IsNullOrEmpty(Label))
                return a;
            return a + ":" + Label.AddQuotesIfNecessary();
        }

        public UmlRelation With(UmlArrowDirections dir)
        {
            Arrow = Arrow.With(dir);
            return this;
        }

        public UmlRelation WithNote(INoteProvider np)
        {
            if (np == null) return this;
            Note           = np.GetNoteText();
            NoteBackground = np.GetNoteBackground();
            return this;
        }

        public UmlRelationEnd Left  { get; set; }
        public UmlRelationEnd Right { get; set; }

        public UmlRelationArrow           Arrow          { get; set; }
        public string                     Label          { get; set; }
        public string                     Note           { get; set; }
        public IUmlFill                   NoteBackground { get; set; }
        public Dictionary<string, object> Metadata       { get; } = new Dictionary<string, object>();

        public string Tag { get; set; }

        [CanBeNull]
        public UmlMember BaseMember { get; set; }
    }
}