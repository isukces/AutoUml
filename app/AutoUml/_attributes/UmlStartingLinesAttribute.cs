using System;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities, AllowMultiple = true)]
public class UmlStartingLinesAttribute : Attribute
{
    public UmlStartingLinesAttribute(string startingLines, ClassLineKind lineKind = ClassLineKind.Double)
    {
        StartingLines = startingLines;
        LineKind      = lineKind;
    }

    public string        StartingLines { get; }
    public ClassLineKind LineKind      { get; }
}
