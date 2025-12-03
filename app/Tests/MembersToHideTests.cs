using AutoUml;
using Xunit;

namespace Tests;

public class MembersToHideTests
{
    [Theory]
    [InlineData(MembersToHide.Circle, "hide circle")]
    [InlineData(MembersToHide.EmptyAttributes, "hide empty attributes")]
    public void T01_Should_create_hide(MembersToHide a, string lines)
    {
        var pumlCommands  = a.GetPumlCommands("hide");
        var expectedLines = lines.Split(',');
        Assert.Equal(expectedLines.Length, pumlCommands.Count);
        for (var index = 0; index < expectedLines.Length; index++)
        {
            var expected = expectedLines[index];
            Assert.Equal(expected, pumlCommands[index]);
        }
    }
}