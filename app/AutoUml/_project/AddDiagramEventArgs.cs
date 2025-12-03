using System;

namespace AutoUml;

public class AddDiagramEventArgs : EventArgs
{
    public required UmlDiagram Diagram { get; init; }
}
