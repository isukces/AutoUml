using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AutoUml;

[Flags]
public enum MembersToHide
{
    EmptyFields = 1,
    EmptyMethods = 2,

    EmptyAttributes = 4,
    EmptyMembers = EmptyFields | EmptyMethods,

    Fields = 8,
    Methods = 16,
    //Attributes = 32,

    Circle = 64,
    Stereotype = 128,

    All = Stereotype * 2 - 1
}

public static class MembersToHideExt
{
    static MembersToHideExt()
    {
        testItems = new[]
        {
            new Test(MembersToHide.EmptyMembers, "empty members"),
            new Test(MembersToHide.EmptyFields, "empty fields"),
            new Test(MembersToHide.EmptyMethods, "empty methods"),
            new Test(MembersToHide.EmptyAttributes, "empty attributes"),

            new Test(MembersToHide.Fields, "fields"),
            new Test(MembersToHide.Methods, "methods"),

            new Test(MembersToHide.Circle, "circle"),
            new Test(MembersToHide.Stereotype, "stereotype"),
        };
    }

    public static IReadOnlyList<string> GetPumlCommands(this MembersToHide src, string prefix)
    {
        prefix = prefix?.Trim() + " ";
        var list = new List<string>();
        foreach (var test in testItems)
        {
            if ((src & test.Member) != test.Member)
                continue;
            list.Add(prefix + test.Command);
            src &= test.Member;
        }

        if ((src & MembersToHide.EmptyMembers) != MembersToHide.EmptyMembers)
        {
        }

        return list;
    }

    private static readonly Test[] testItems;

    [ImmutableObject(true)]
    private class Test
    {
        public Test(MembersToHide member, string command)
        {
            Member  = member;
            Command = command;
        }

        public override string ToString()
        {
            return "Member={Member}, Command={Command}";
        }

        public MembersToHide Member { get; }

        public string Command { get; }
    }
}