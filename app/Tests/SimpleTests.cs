using System;
using System.Collections.Generic;
using AutoUml;
using Xunit;

namespace Tests;

public class SimpleTests
{
    [Fact]
    public void T01_Should_convert_int_array_to_string()
    {
        var type = typeof(int);
        var name = type.GetDiagramName(q => null);
        Assert.Equal("int", name);

        type = typeof(int[]);
        name = type.GetDiagramName(q => null);
        Assert.Equal("int[]", name);

        type = typeof(int[][]);
        name = type.GetDiagramName(q => null);
        Assert.Equal("int[][]", name);

        type = typeof(int[,]);
        name = type.GetDiagramName(q => null);
        Assert.Equal("int[,]", name);

        type = typeof(int[,][]);
        name = type.GetDiagramName(q => null);
        Assert.Equal("int[,][]", name);

        type = typeof(int[,][,,,]);
        name = type.GetDiagramName(q => null);
        Assert.Equal("int[,][,,,]", name);

        type = typeof(List<int[,][,,,]>);
        name = type.GetDiagramName(q => null);
        Assert.Equal("List<int[,][,,,]>", name);
    }


    [Theory]
    [InlineData(typeof(List<int>))]
    [InlineData(typeof(IList<int>))]
    [InlineData(typeof(ICollection<int>))]
    [InlineData(typeof(IReadOnlyList<int>))]
    [InlineData(typeof(IReadOnlyCollection<int>))]
    [InlineData(typeof(int[]))]
    [InlineData(typeof(int[,]))]
    public void T02_Should_recognize_collections(Type t)
    {
        var ex = new TypeExInfo(t, false);
        Assert.True(ex.IsCollection);
        Assert.Equal(typeof(int), ex.ElementType);
    }
}
