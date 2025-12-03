using System.Collections.Generic;
using AutoUml;

namespace Tests;

[UmlDiagram("Test3", Note = "Note from annotation", NoteLocation = NoteLocation.Right)]
[UmlAddMeta("hello", "world")]
public class Order3
{
    [UmlRelation(ForceAddToDiagram = true,
        Note = "Note on relation\nfrom UmlRelationAtribute",
        NoteBackground = "#ffe0f0",
        // LeftSign  =  ForceArrowEnd.Multiple, RightSign = ForceArrowEnd.Multiple,
        ArrowDirection = UmlArrowDirections.Right
        // IsDotted = ChangeDecision.Yes, LeftSignDescription="one"
    )]
    public CompanyInfo Customer { get; set; }

    [UmlRelation(
        ForceAddToDiagram = true,
        LeftSign = ForceArrowEnd.DiamondBlack, RightSign = ForceArrowEnd.ArrowWhite,
        LeftSignDescription = "bla 1",
        RightSignDescription = "bla 2",
        ArrowDirection = UmlArrowDirections.Up,
        Color = "yellow",
        Tag = "MyId"
    )]
    public CompanyInfo Customer2 { get; set; }

    [UmlRelation(UmlRelationKind.Composition, ArrowLength = 5)]
    public List<OrderItem3> Items { get; set; }
}

[UmlDiagram("Test3")]
[UmlNote("Note from UmlNote", NoteLocation = NoteLocation.Left)]
public class OrderItem3
{
}

[UmlDiagram("Test4")]
public class CompanyInfo
{
    public string Name { get; set; }
}
