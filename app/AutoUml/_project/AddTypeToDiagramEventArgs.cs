using System;

namespace AutoUml;

public class AddTypeToDiagramEventArgs : EventArgs
{
    public required UmlEntity  Info    { get; init; }
    public required UmlDiagram Diagram { get; init; }
}
