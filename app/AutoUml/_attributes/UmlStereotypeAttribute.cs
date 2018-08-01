using System;

namespace AutoUml
{
    [AttributeUsage(AttributesConsts.Entities)]
    public class UmlStereotypeAttribute : Attribute
    {
        public UmlStereotypeAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}