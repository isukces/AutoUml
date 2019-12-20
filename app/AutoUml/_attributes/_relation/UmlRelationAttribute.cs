using System;

namespace AutoUml
{
    /// <summary>
    /// Use it to modify uml relation made from property, field or method result
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public class UmlRelationAttribute : BaseRelationAttribute
    {
        public UmlRelationAttribute(UmlRelationKind kind = UmlRelationKind.Aggregation,
            UmlArrowDirections arrowDirection = UmlArrowDirections.Auto) : base(kind)
        {
            ArrowDirection = arrowDirection;
        }

        public Multiplicity       Multiple          { get; set; }

    }
}