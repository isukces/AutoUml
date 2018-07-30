using System;
using System.Collections.Generic;

namespace AutoUml
{
    public class TypeExInfo
    {
        public TypeExInfo(Type originalType)
        {
            ElementType = OriginalType = originalType;

            IsGeneric = originalType.IsGenericType;
            if (IsGeneric)
            {
                GenericTypeDef = originalType.GetGenericTypeDefinition();
                if (GenericTypeDef == typeof(Nullable<>))
                {
                    ElementType = originalType.GetGenericArguments()[0];
                    return;
                }
            }

            var t = GetListElement(originalType);
            if (t != null)
            {
                ElementType  = t;
                IsCollection = true;
            }
        }

        public static Type GetListElement(Type type)
        {
            if (type.IsArray)
            {
                var el = type.GetElementType();
                var el2 = GetListElement(el);
                return el2 ?? el;
            }
            while (true)
            {
                if (type == null) return null;                
                if (type.IsGenericType)
                {
                    var ig = type.GetGenericTypeDefinition();
                    if (ig == typeof(IList<>) 
                        || ig == typeof(IReadOnlyList<>) 
                        || ig == typeof(IReadOnlyCollection<>) 
                        || ig == typeof(ICollection<>))
                        return type.GetGenericArguments()[0];
                }

                foreach (var i in type.GetInterfaces())
                {
                    var x = GetListElement(i);
                    if (x != null) return x;
                }

                type = type.BaseType;
            }
        }


        public Type OriginalType   { get; }
        public Type ElementType    { get; }
        public Type GenericTypeDef { get; }
        public bool IsGeneric      { get; }
        public bool IsCollection   { get; }
    }
}