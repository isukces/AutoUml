using System;

namespace AutoUml
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class UmlPackageStyleAttribute : Attribute
    {
        public UmlPackageStyleAttribute(string packageName, UmlPackageKind kind, string diagramName = null)
        {
            PackageName = packageName;
            Kind        = kind;
            DiagramName = diagramName;
        }

        public bool CanBeUsedFor(UmlDiagram diagram)
        {
            return string.IsNullOrEmpty(DiagramName)
                   || string.Equals(DiagramName, diagram?.Name.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public string         PackageName { get; }
        public UmlPackageKind Kind        { get; }

        public string DiagramName
        {
            get => _diagramName;
            set => _diagramName = value?.Trim();
        }

        private string _diagramName;
    }
}