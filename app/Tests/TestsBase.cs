using System.IO;
using System.Runtime.CompilerServices;
using AutoUml;

namespace Tests
{
    public class TestsBase
    {
        protected static void Save(string puml, string shortName = null, [CallerMemberName] string name = null,
            [CallerFilePath] string fn = null)
        {
            if (!string.IsNullOrEmpty(shortName))
                name = name + "-" + shortName;
            var fi  = new FileInfo(fn).Directory.SearchFoldersUntilFileExists(".gitignore");
            var fn2 = new FileInfo(Path.Combine(fi.FullName, "docs", "testsResults", name + ".puml"));
            if (fn2.SaveContentIfDifferent(puml))
            {
                var v = new PlantUmlRunner
                {
                    GraphVizDot = @"c:\Program Files (x86)\Graphviz2.38\bin\dot.exe",
                    PlantUmlJar = @"c:\Program Files (x86)\plantuml-jar-mit-1.2018.8\plantuml.jar",
                };
                v.Run(fn2).WaitForExit();
            }
        }
    }
}