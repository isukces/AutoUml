using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUml
{
    public abstract class UmlMember : IMetadataContainer
    {
        public abstract MemberInfo GetMemberInfo();
        public abstract void WriteTo(CodeWriter cf, UmlDiagram diagram);
        public int                        Group      { get; set; }
        public string                     Name       { get; set; }
        public bool                       HideOnList { get; set; }
        public Dictionary<string, object> Metadata   { get; } = new Dictionary<string, object>();
        
        
        public UmlMemberKind Kind { get; set; }
        
        
        protected string GetCodePrefix()
        {
            switch (Kind)
            {
                case UmlMemberKind.Normal:
                    return "";
                case UmlMemberKind.Abstract:
                    return "{abstract} ";
                case UmlMemberKind.Static:
                    return "{static} ";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum UmlMemberKind
    {
        Normal,
        Abstract,
        Static
    }
}