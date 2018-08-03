using System.Collections.Generic;
using System.Reflection;

namespace AutoUml
{
    public abstract class UmlMember : ICustomDataContainer
    {
        public abstract void WriteTo(CodeWriter cf, UmlProjectDiagram diagram);
        public int                        Group      { get; set; }
        public string                     Name       { get; set; }
        public bool                       HideOnList { get; set; }
        public Dictionary<string, object> CustomData { get; } = new Dictionary<string, object>();

        public abstract MemberInfo GetMemberInfo();
    }
}