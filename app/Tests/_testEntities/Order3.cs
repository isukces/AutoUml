using System.Collections.Generic;
using AutoUml;

namespace Tests
{
    [UmlDiagram("Test3", Note = "Note from annotation", NoteLocation = NoteLocation.Right)]
    public class Order3
    {
        [UmlRelation(UmlRelationKind.Aggregation,
            ForceAddToDiagram = true,
            Note              = "Note on relation\nfrom UmlRelationAtribute",
            NoteBackground    = "#ffe0f0",
            ArrowDirection    = UmlArrowDirections.Right)]
        public CompanyInfo Customer { get; set; }

        [UmlRelation(UmlRelationKind.Composition)]
        public List<OrderItem3> Items { get; set; }
    }

    [UmlDiagram("Test3")]
    [UmlNote("Note from UmlNote", NoteLocation = NoteLocation.Left)]
    public class OrderItem3
    {
    }

    public class CompanyInfo
    {
        public string Name { get; set; }
    }
}