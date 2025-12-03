using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities, AllowMultiple = true)]
[Conditional("AUTOUML_ANNOTATIONS")]
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
