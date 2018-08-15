using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutoUml
{
    public class PlantUmlRunner
    {
        public static bool IsPngUpToDate(FileInfo puml)
        {
            var png = new FileInfo(puml.FullName.Substring(0, puml.FullName.Length - 5) + ".png");
            if (!png.Exists) return false;
            return png.LastWriteTime >= puml.LastWriteTime;
        }

        private static string Quote(string x)
        {
            if (string.IsNullOrEmpty(x))
                return x;
            if (x.IndexOf(' ') >= 0)
                return "\"" + x + "\"";
            return x;
        }

        public string GetBatch(FileInfo puml)
        {
            var lines = new StringBuilder();
            if (puml.Directory != null)
            {
                lines.AppendLine(puml.Directory.FullName.Substring(0, 2));
                lines.AppendLine(string.Format("cd {0}", Quote(puml.Directory.FullName)));
            }

            if (!string.IsNullOrEmpty(GraphVizDot))
                lines.AppendLine(string.Format("set GRAPHVIZ_DOT={0}", Quote(GraphVizDot)));

            lines.AppendLine(string.Format("{0} -jar {1} -charset UTF-8 {2}",
                Quote(JavaExe),
                Quote(PlantUmlJar),
                Quote(puml.Name)));
            return lines.ToString();
        }

        public Process Run(FileInfo puml)
        {
            var startInfo = new ProcessStartInfo();
            if (!string.IsNullOrEmpty(GraphVizDot))
                startInfo.EnvironmentVariables["GRAPHVIZ_DOT"] = GraphVizDot;
            startInfo.UseShellExecute = false;
            startInfo.FileName        = JavaExe;
            startInfo.Arguments       = string.Format("-jar {0} -charset UTF-8 {1}", Quote(PlantUmlJar), Quote(puml.Name));
            if (puml.Directory != null)
                startInfo.WorkingDirectory = puml.Directory.FullName;
            startInfo.UseShellExecute        = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow         = true;
            var p = new Process {StartInfo = startInfo};
            p.Start();
            return p;
        }

        /// <summary>
        ///     Dot.exe location i.e. c:\Program Files (x86)\Graphviz2.38\bin\dot.exe
        /// </summary>
        public string GraphVizDot { get; set; }

        /// <summary>
        ///     i.e. c:\Program Files (x86)\plantuml-jar-mit-1.2018.8\plantuml.jar
        /// </summary>
        public string PlantUmlJar { get; set; }

        public string JavaExe { get; set; } = "java.exe";
    }
}