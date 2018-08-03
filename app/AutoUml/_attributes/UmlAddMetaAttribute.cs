using System;

namespace AutoUml
{
    public class UmlAddMetaAttribute : Attribute
    {
        public UmlAddMetaAttribute(string name, string valueString)
        {
            Name        = name;
            ValueString = valueString;
        }

        public string Name        { get; }
        public string ValueString { get; }
    }
}