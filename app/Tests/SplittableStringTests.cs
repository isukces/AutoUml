using AutoUml;
using Xunit;

namespace Tests;

public class SplittableStringTests
{
    [Fact]
    public void T01_Should_make_empty()
    {
        var q = SplittableString.Make(null);
        Assert.Null(q);
        q = SplittableString.Make();
        Assert.Null(q);
    }

    [Fact]
    public void T02_Should_add_empty()
    {
        var          q        = SplittableString.Make("Bla ", "bla");
        const string expected = "Bla bla";
        Assert.Equal(expected, q.ToString());
        {
            var w = q + (string?)null;
            Assert.True(ReferenceEquals(q, w));
            Assert.Equal(expected, q.ToString());
        }
        {
            var w = q + string.Empty;
            Assert.True(ReferenceEquals(q, w));
            Assert.Equal(expected, q.ToString());
        }
        {
            var w = (string?)null + q;
            Assert.True(ReferenceEquals(q, w));
            Assert.Equal(expected, q.ToString());
        }
        {
            var w = string.Empty + q;
            Assert.True(ReferenceEquals(q, w));
            Assert.Equal(expected, q.ToString());
        }
    }

    [Fact]
    public void T03_Should_add()
    {
        var q   = SplittableString.Make("Bla ", "bla");
        var w   = "Other";
        var sum = q + "Other";
        Assert.False(ReferenceEquals(q, sum));
        Assert.Equal("Bla blaOther", sum.ToString());
    }
}
