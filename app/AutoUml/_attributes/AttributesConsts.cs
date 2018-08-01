using System;

namespace AutoUml
{
    public static class AttributesConsts
    {
        public const AttributeTargets Entities =
            AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum;
    }
}