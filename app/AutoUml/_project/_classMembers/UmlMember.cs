using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoUml;

public abstract class UmlMember : IMetadataContainer
{
    protected string GetCodePrefix()
    {
        return GetCodePrefixA() + GetCodePrefixB();

        string GetCodePrefixA()
        {
            switch (Visibility)
            {
                case VisibilityFlag.None: return "";
                case VisibilityFlag.Private: return "-";
                case VisibilityFlag.Protected: return "#";
                case VisibilityFlag.PackagePrivate: return "~";
                case VisibilityFlag.Public: return "+";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        string GetCodePrefixB()
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

    public abstract MemberInfo? GetMemberInfo();
    public abstract void WriteTo(CodeWriter cf, UmlDiagram diagram);

    public int     Group      { get; set; }
    public string? Name       { get; set; }
    public bool    HideOnList { get; set; }


    public UmlMemberKind  Kind       { get; set; }
    public VisibilityFlag Visibility { get; set; }

    public Dictionary<string, object> Metadata { get; } = new();
}

public enum VisibilityFlag
{
    None,
    Private,
    Protected,
    PackagePrivate,
    Public
}

public enum UmlMemberKind
{
    Normal,
    Abstract,
    Static
}
