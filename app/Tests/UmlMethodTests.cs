using System;
using System.Collections.Generic;
using AutoUml;
using Xunit;

namespace Tests;

public class UmlMethodTests
{
    [Fact]
    public void T01_Should_convert_very_long_method_to_string()
    {
        var mi    = GetType().GetMethod("VeryLongMethod");
        var code  = mi.MethodToUml(a => a.Name);
        var sink  = new List<string>();
        var lines = code.MakeAction(30, (lineIndex, text) => { sink.Add(text); });
        Assert.Equal(7, lines);
        var json = sink.ToCompareJson();
        Assert.Equal(
            "['Void VeryLongMethod(String x1,','Int32 x2,Single x3,','DateTime x4,String y1,','Int32 y2,Single y3,','DateTime y4,String z1,','Int32 z2,Single z3,','DateTime z4)']",
            json);
    }

    public void VeryLongMethod(
        string x1, int x2, float x3, DateTime x4,
        string y1, int y2, float y3, DateTime y4,
        string z1, int z2, float z3, DateTime z4
    )
    {
        throw new NotImplementedException();
    }
}