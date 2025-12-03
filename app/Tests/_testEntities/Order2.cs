using System.Collections.Generic;
using AutoUml;

namespace Tests;

[UmlDiagram("Test2", Note = "Note from annotation", NoteLocation = NoteLocation.Right)]
public class Order2
{
    public List<OrderItem2> Items { get; set; }
}

[UmlDiagram("Test2")]
[UmlNote("Note from UmlNote", NoteLocation = NoteLocation.Left)]
public class OrderItem2
{
}