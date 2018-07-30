using System;

namespace AutoUml
{
    public class AddTypeToDiagramEventArgs : EventArgs
    {
        public UmlEntity         Info    { get; set; }
        public UmlProjectDiagram Diagram { get; set; }
    }
}