using System;
using System.Collections.Generic;

namespace AutoUml;

public class TypeExInfo
{
    public TypeExInfo(Type originalType, bool doNotResolveCollection)
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

        if (doNotResolveCollection)
            return;
        var t = GetListElement(originalType);
        if (t is null) return;
        ElementType  = t;
        IsCollection = true;
    }

    public static Type GetListElement(Type type)
    {
        {
            var h = OnGetListElement;
            if (h != null)
            {
                var args = new OnGetListElementEventArgs(type);
                h.Invoke(null, args);
                if (args.Handled)
                    return args.ElementType;
            }
        }
        if (type.IsArray)
        {
            var el  = type.GetElementType();
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

    public static EventHandler<OnGetListElementEventArgs> OnGetListElement;

    public class OnGetListElementEventArgs
    {
        public OnGetListElementEventArgs(Type type)
        {
            Type = type;
        }

        public Type Type        { get; }
        public bool Handled     { get; set; }
        public Type ElementType { get; set; }
    }
}
