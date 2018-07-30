using System;

namespace AutoUml
{
    public class AddDiagramEventArgs : EventArgs
    {
        public UmlProjectDiagram Diagram { get; set; }
    }
}