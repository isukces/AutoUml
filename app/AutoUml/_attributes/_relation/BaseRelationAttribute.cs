using System;

namespace AutoUml;

public abstract class BaseRelationAttribute : Attribute, INoteProvider
{
    protected BaseRelationAttribute(UmlRelationKind kind)
    {
        Kind = kind;
    }

    IUmlFill? INoteProvider.GetNoteBackground()
    {
        return UmlColor.FromString(NoteBackground).ToFill();
    }


    string? INoteProvider.GetNoteText()
    {
        return Note;
    }

    public string? Name { get; set; }

    public string? Note { get; set; }

    public string? NoteBackground { get; set; }

    public UmlRelationKind Kind { get; }

    public UmlArrowDirections ArrowDirection { get; set; }

    public bool ForceAddToDiagram { get; set; }

    public Type? RelatedType { get; set; }

    /// <summary>
    ///     Creates one-to-one relation to collection type instead of one-to-many relation to
    ///     collection's element type
    /// </summary>
    public bool DoNotResolveCollections { get; set; }


    public int ArrowLength { get; set; } = 2;

    public ForceArrowEnd  LeftSign  { get; set; } = ForceArrowEnd.NotSet;
    public ForceArrowEnd  RightSign { get; set; } = ForceArrowEnd.NotSet;
    public ChangeDecision IsDotted  { get; set; } = ChangeDecision.Auto;

    public string? LeftSignDescription  { get; set; }
    public string? RightSignDescription { get; set; }

    public string? Color { get; set; }


    /// <summary>
    ///     Some tag added to relation object to identify it
    /// </summary>
    public string? Tag { get; set; }
}
