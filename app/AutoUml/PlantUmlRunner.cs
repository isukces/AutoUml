using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace AutoUml
{
    public class PlantUmlRunner
    {
        public static string GetFileExtension(AutoUmlOutputFormat format)
        {
            if (format == AutoUmlOutputFormat.Utxt)
                format = AutoUmlOutputFormat.Txt;
            if (format == AutoUmlOutputFormat.EpsText)
                format = AutoUmlOutputFormat.Eps;
            if (format == AutoUmlOutputFormat.Braille)
                format = AutoUmlOutputFormat.Png;
            if (format == AutoUmlOutputFormat.LatexNoPreamble)
                format = AutoUmlOutputFormat.Latex;
            return $".{format}".ToLower();
        }

        public static FileInfo GetImageFileInfo(FileInfo puml, AutoUmlOutputFormat format)
        {
            var ext = GetFileExtension(format);
            return GetImageFileInfo(puml, ext);
        }


        private static FileInfo GetImageFileInfo(FileInfo puml, string extension)
        {
            var png = new FileInfo(puml.FullName.Substring(0, puml.FullName.Length - 5) + extension);
            return png;
        }

        [NotNull]
        public static string GetOutputFormatOption(AutoUmlOutputFormat format)
        {
            switch (format)
            {
                case AutoUmlOutputFormat.Png:
                case AutoUmlOutputFormat.Svg:
                case AutoUmlOutputFormat.Eps:
                case AutoUmlOutputFormat.Xmi:
                case AutoUmlOutputFormat.Pdf:
                case AutoUmlOutputFormat.Vdx:
                case AutoUmlOutputFormat.Scxml:
                case AutoUmlOutputFormat.Latex:
                case AutoUmlOutputFormat.Braille:
                case AutoUmlOutputFormat.Html:
                case AutoUmlOutputFormat.Txt:
                    return format.ToString().ToLower();

                case AutoUmlOutputFormat.EpsText:
                    return "eps:text";
                case AutoUmlOutputFormat.LatexNoPreamble:
                    return "latex:nopreamble";

                case AutoUmlOutputFormat.Utxt:
                    return "txt";

                default: throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }


        public static bool IsImageUpToDate(FileInfo puml, AutoUmlOutputFormat format)
        {
            var png = GetImageFileInfo(puml, format);
            if (!png.Exists) return false;
            if (png.Length == 0) return false;
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

        public string GetBatch(FileInfo puml, AutoUmlOutputFormat format = AutoUmlOutputFormat.Png)
        {
            var lines = new StringBuilder();
            if (puml.Directory != null)
            {
                lines.AppendLine(puml.Directory.FullName.Substring(0, 2));
                lines.AppendLine(string.Format("cd {0}", Quote(puml.Directory.FullName)));
            }
            var arguments = GetArguments(puml, format);
            if (!string.IsNullOrEmpty(GraphVizDot))
                lines.AppendLine(string.Format("set GRAPHVIZ_DOT={0}", Quote(GraphVizDot)));
            lines.AppendLine(arguments);
            return lines.ToString();
        }

        private string GetArguments(FileInfo puml, AutoUmlOutputFormat format)
        {
            var items = new List<string>
            {
                JavaExe,
                "-jar",
                PlantUmlJar,
                "-charset",
                "UTF-8"
            };
            if (format != AutoUmlOutputFormat.Png)
                items.Add("-t" + GetOutputFormatOption(format));
            items.Add(puml.Name);
            return string.Join(" ", items.Select(Quote));
        }

        public Process Run(FileInfo puml, AutoUmlOutputFormat format = AutoUmlOutputFormat.Png)
        {
            var startInfo = new ProcessStartInfo();
            if (!string.IsNullOrEmpty(GraphVizDot))
                startInfo.EnvironmentVariables["GRAPHVIZ_DOT"] = GraphVizDot;
            startInfo.UseShellExecute = false;
            startInfo.FileName        = JavaExe;
            
            startInfo.Arguments = GetArguments(puml, format);
            if (puml.Directory != null)
                startInfo.WorkingDirectory = puml.Directory.FullName;
            startInfo.UseShellExecute        = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow         = true;
            var p = new Process { StartInfo = startInfo };
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

    public enum AutoUmlOutputFormat
    {
        Png,
        Svg,
        Eps,
        EpsText,
        Pdf,
        Vdx,
        Xmi,
        Scxml,
        Html,
        Txt,
        Utxt,
        Latex,
        LatexNoPreamble,
        Braille
    }
}
