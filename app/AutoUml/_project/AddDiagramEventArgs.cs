using System;

namespace AutoUml;

public class AddDiagramEventArgs : EventArgs
{
    public UmlDiagram Diagram { get; set; }
}
