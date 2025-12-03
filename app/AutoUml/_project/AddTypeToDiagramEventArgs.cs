using System;

namespace AutoUml;

public class AddTypeToDiagramEventArgs : EventArgs
{
    public UmlEntity  Info    { get; set; }
    public UmlDiagram Diagram { get; set; }
}
