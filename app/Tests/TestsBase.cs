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
            fn2.SaveContentIfDifferent(puml);
        }
    }
}