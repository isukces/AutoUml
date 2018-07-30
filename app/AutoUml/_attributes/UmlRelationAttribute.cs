using System;

namespace AutoUml
{
    public class UmlRelationAttribute : Attribute
    {
        public UmlRelationAttribute(UmlRelationKind kind = UmlRelationKind.Aggregation,
            UmlArrowDirections arrowDirection = UmlArrowDirections.Auto)
        {
            Kind           = kind;
            ArrowDirection = arrowDirection;
        }

        public UmlRelationKind Kind { get; }

        public UmlArrowDirections ArrowDirection { get; set; }

        public string Note     { get; set; }
        public bool?  Multiple { get; set; }
    }
}