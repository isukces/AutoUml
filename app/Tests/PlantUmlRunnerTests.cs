using System.IO;
using AutoUml;
using Xunit;

namespace Tests
{
    public class PlantUmlRunnerTests
    {
        [Fact]
        public void T01_Should_create_batch()
        {
            var r = new PlantUmlRunner
            {
                JavaExe     = "java.exe",
                GraphVizDot = @"c:\Program Files (x86)\Graphviz2.38\bin\dot.exe",
                PlantUmlJar = @"c:\Program Files (x86)\plantuml-jar-mit-1.2018.8\plantuml.jar"
            };
            var batch         = r.GetBatch(new FileInfo(@"c:\temp\diagram.puml"));
            var expectedBatch = @"c:
cd c:\temp
set GRAPHVIZ_DOT=""c:\Program Files (x86)\Graphviz2.38\bin\dot.exe""
java.exe -jar ""c:\Program Files (x86)\plantuml-jar-mit-1.2018.8\plantuml.jar"" -charset UTF-8 diagram.puml
";
            Assert.Equal(expectedBatch, batch);
        }
    }
}