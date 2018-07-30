using System.Collections.Generic;
using System.IO;

namespace AutoUml
{
    public class PlantUmlFile
    {
        public void SaveIfDifferent(string filename)
        {
            var txt = Code;
            if (File.Exists(filename))
            {
                var existing = File.ReadAllText(filename);
                if (txt == existing)
                    return;
            }

            new FileInfo(filename).Directory?.Create();
            File.WriteAllText(filename, txt);
        }

        public override string ToString()
        {
            return Code;
        }

        public string Code
        {
            get
            {
                var cf = new CodeWriter();
                cf.Writeln("@startuml");
                cf.Add(Top);
                cf.Add(Classes);
                foreach (var t in Relations)
                {
                    cf.Writeln(t.ToString());
                    if (string.IsNullOrEmpty(t.Note)) continue;
                    cf.Writeln("note on link " + t.NoteColor);
                    foreach (var line in t.Note.Split('\n', '\r'))
                        if (!string.IsNullOrEmpty(line))
                            cf.Writeln(line);
                    cf.Writeln("end note");
                }

                cf.Writeln("@enduml");
                return cf.Code;
            }
        }

        public CodeWriter        Top       { get; } = new CodeWriter();
        public List<UmlRelation> Relations { get; } = new List<UmlRelation>();
        public CodeWriter        Classes   { get; } = new CodeWriter();
    }
}