using System;

namespace AutoUml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple =
        true)]
    public class UmlDiagramAttribute : Attribute
    {
        public UmlDiagramAttribute(string diagramName)
        {
            DiagramName = diagramName;
        }

        public string DiagramName { get; }

        public string BackgroundColor { get; set; }
    }
}