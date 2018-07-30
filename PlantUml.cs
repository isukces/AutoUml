using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using isukces.code;
using isukces.Linq;
using Pd.Interfaces;

namespace Pd.Startup
{
#if DEBUG
    public static class PlantUmlEx
    {
        public static string AddQuotes(this string x)
        {
            if (x == null)
                return null;
            return "\"" + x + "\"";
        }
    }

    public class PlantUml
    {
        private static Type GetListElement(Type type)
        {
            while (true)
            {
                if (type == null) return null;
                if (type.IsGenericType)
                {
                    var ig = type.GetGenericTypeDefinition();
                    if (ig == typeof(IList<>) || ig == typeof(IReadOnlyList<>) || ig == typeof(ICollection<>))
                        return type.GetGenericArguments()[0];
                }

                foreach (var i in type.GetInterfaces())
                {
                    var x = GetListElement(i);
                    if (x != null) return x;
                }

                type = type.BaseType;
            }
        }

        private static void WriteSkin(PlantUmlResult r)
        {
            var cf = r.Top;
            cf.Writeln("skinparam classFontSize 18");
            cf.Writeln("skinparam class {");
            cf.Writeln("FontSize 20");
            cf.Writeln("ArrowFontSize 20");
            cf.Writeln("FontName Buxton Sketch");
            cf.Writeln("ArrowFontName Buxton Sketch");
            cf.Writeln("BackgroundColor white");
            cf.Writeln("BorderColor #000040");
            cf.Writeln("}");
            cf.Writeln("skinparam handwritten true");
        }

        public void Add(Type type, string name, string bgColor)
        {
            _types.Add(type, new Info2(name, bgColor)
            {
                OrderIndex = _types.Count
            });
        }

        public void GenerateFile(string filename, string scale, string title)
        {
            var cf = new PlantUmlResult();

            WriteSkin(cf);
            cf.Top.Writeln("scale " + scale);
            if (!string.IsNullOrEmpty(title))
            {
                cf.Top.Writeln("title");
                cf.Top.Writeln(' ' + title);
                cf.Top.Writeln("end title");
            }

            var iter = _types.OrderBy(a => a.Value.OrderIndex).Select(a => a.Key)
                .ToListCastOrConvert(_types.Count);
            var alreadyProcessed = new HashSet<Type>();

            void Process1(IEnumerable<Type> typesList)
            {
                foreach (var t in typesList)
                {
                    var list = AddTypeToGraph(cf, t, cf.relations, alreadyProcessed);
                    Process1(list);
                }
            }

            Process1(iter);

            cf.SaveIfDifferent(filename);
        }

        private IEnumerable<Type> AddTypeToGraph(PlantUmlResult rr, Type t, List<UmlRelation> relations,
            HashSet<Type> processed)
        {
            var result = new List<Type>();
            if (!_types.TryGetValue(t, out var info))
                return result;
            if (!processed.Add(t))
                return result;

            var cf = rr.Classes;
            cf.Open(GetClassOpen(t));
            foreach (var pi in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (SkipDrawing(t, pi.DeclaringType))
                    continue;
                var att  = pi.GetCustomAttribute<UmlRelationAttribute>();
                var kind = att?.Kind ?? UmlRelationKind.AggregationWithArrow;
                var rel  = FindRelation(pi.PropertyType, kind, att, pi.Name, t);
                if (rel != null)
                {
                    relations.Add(rel);
                    result.Add(pi.PropertyType);
                    continue;
                }

                cf.Writeln(GetTypeName(pi.PropertyType) + " " + pi.Name);
            }

            foreach (var mi in t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                if (mi.IsSpecialName)
                    continue;
                if (mi.DeclaringType == typeof(object))
                    continue;
                if (SkipDrawing(t, mi.DeclaringType))
                    continue;
                cf.Writeln(MethodToUml(mi));

                var att = mi.GetCustomAttribute<UmlRelationAttribute>();
                if (att != null)
                {
                    var kind = att.Kind;
                    var rel  = FindRelation(mi.ReturnType, kind, att, mi.Name + "() >", t);
                    if (rel != null)
                    {
                        var a = rel.Arrow;
                        a.Dotted  = true;
                        rel.Arrow = a;

                        relations.Add(rel);
                        result.Add(mi.ReturnType);
                    }
                }
            }

            foreach (var i in t.GetCustomAttributes<UmlAddRelationAttribute>())
            {
                var rel = new UmlRelation
                {
                    Left      = new UmlRelationEnd(GetTypeName(t)),
                    Right     = new UmlRelationEnd(GetTypeName(i.RelatedType)),
                    Arrow     = UmlRelationArrow.GetRelationByKind(i.Kind),
                    Label     = i.Name,
                    Note      = i.Note,
                    NoteColor = i.NoteColor
                };
                relations.Add(rel);
            }

            cf.Close();

            foreach (var i in t.GetCustomAttributes<UmlNoteAttribute>())
            {
                // cf.Writeln("-- note --");
                cf.Writeln("note bottom of " + GetTypeName(t));
                foreach (var line in i.Text.Split('\n', '\r'))
                    if (!string.IsNullOrEmpty(line))
                        cf.Writeln(line);
                cf.Writeln("end note");
            }

            if (t.BaseType != null && _types.ContainsKey(t.BaseType))
                relations.Add(Inherits(t.BaseType, t).With(UmlArrowDirections.Up));
            foreach (var i in t.GetInterfaces())
            {
                if (_types.ContainsKey(i))
                    relations.Add(Inherits(i, t).With(UmlArrowDirections.Up));
            }

            return result;
        }

        private UmlRelation Aggregation(Type owner, Type component, UmlArrowDirections? direction = null,
            string label = null, string ownerLabel = null,
            string componentLabel = null)
        {
            var isManyComponent = componentLabel == "0..n";
            if (isManyComponent)
                componentLabel = null;

            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(GetTypeName(component), componentLabel),
                Arrow = isManyComponent ? UmlRelationArrow.AggregationLeftMany : UmlRelationArrow.AggregationLeft,
                Label = label
            };
            if (direction != null)
                rel.Arrow = rel.Arrow.With(direction.Value);
            return rel;
        }

        private UmlRelation Composition(Type owner, Type component, UmlArrowDirections? direction = null,
            string label = null, string ownerLabel = null,
            string componentLabel = null)
        {
            var isManyComponent = componentLabel == "0..n";
            if (isManyComponent)
                componentLabel = null;
            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(GetTypeName(owner), ownerLabel),
                Right = new UmlRelationEnd(GetTypeName(component), componentLabel),
                Arrow = isManyComponent ? UmlRelationArrow.CompositionLeftMany : UmlRelationArrow.CompositionLeft,
                Label = label
            };
            if (direction != null)
                rel.Arrow = rel.Arrow.With(direction.Value);
            return rel;
        }

        private UmlRelation FindRelation(Type resultType, UmlRelationKind kind, UmlRelationAttribute att, string name,
            Type t)
        {
            if (_types.ContainsKey(resultType))
            {
                var tmp = kind == UmlRelationKind.CompositionMany
                    ? Composition(t, resultType, att?.ArrowDirection, name)
                    : Aggregation(t, resultType, att?.ArrowDirection, name);
                tmp.Arrow = tmp.Arrow.With(att?.ArrowDirection ?? UmlArrowDirections.Auto);
                tmp.Note  = att?.Note;
                return tmp;
            }

            var t1 = GetListElement(resultType);
            if (t1 != null && _types.ContainsKey(t1))
            {
                var tmp = kind == UmlRelationKind.CompositionMany
                    ? Composition(t, t1, att?.ArrowDirection, name, "1", "0..n")
                    : Aggregation(t, t1, att?.ArrowDirection, name, "1", "0..n");

                tmp.Note = att?.Note;
                return tmp;
            }

            return null;
        }

        private string GetClassOpen(Type type)
        {
            var color   = _types.TryGetValue(type, out var info) ? info.BgColor : null;
            var append2 = "";
            var items   = new List<string>();
            if (type.IsInterface)
                items.Add("interface");
            else if (type.IsEnum)
                items.Add("enum");
            else if (type.IsValueType)
            {
                items.Add("class");
                append2 = "<< (S,#FF7700) struct >>";
            }
            else
            {
                if (type.IsAbstract)
                    items.Add("abstract");
                items.Add("class");
            }

            items.Add(GetTypeName(type));
            if (!string.IsNullOrEmpty(append2))
                items.Add(append2);
            if (!string.IsNullOrEmpty(color))
                items.Add("#" + color);
            return string.Join(" ", items);
        }

        private string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                var gt = type.GetGenericTypeDefinition();
                if (gt == typeof(Nullable<>))
                    return GetTypeName(type.GetGenericArguments()[0]) + "?";
                var name1 = gt.Name.Split('`').First();
                return name1 + "<" + string.Join(",", type.GetGenericArguments().Select(GetTypeName)) + ">";
            }

            if (type == typeof(object)) return "object";
            if (type == typeof(void)) return "void";
            if (type == typeof(int)) return "int";
            if (type == typeof(uint)) return "uint";
            if (type == typeof(long)) return "long";
            if (type == typeof(ulong)) return "ulong";
            if (type == typeof(short)) return "short";
            if (type == typeof(ushort)) return "ushort";
            if (type == typeof(byte)) return "byte";
            if (type == typeof(sbyte)) return "sbyte";
            if (type == typeof(char)) return "char";
            if (type == typeof(string)) return "string";
            if (type == typeof(float)) return "float";
            if (type == typeof(double)) return "double";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(bool)) return "bool";

            if (_types.TryGetValue(type, out var info) && !string.IsNullOrEmpty(info.Name))
                return info.Name;
            return type.Name;
        }

        private UmlRelation Inherits(Type baseClass, Type subClass)
        {
            var rel = new UmlRelation
            {
                Left  = new UmlRelationEnd(GetTypeName(subClass)),
                Right = new UmlRelationEnd(GetTypeName(baseClass)),
                Arrow = UmlRelationArrow.InheritRight
            };
            return rel;
        }

        private string MethodToUml(MethodInfo i)
        {
            var args = from ii in i.GetParameters()
                select GetTypeName(ii.ParameterType) + " " + ii.Name;
            var args2 = string.Join(",", args);
            return $"{GetTypeName(i.ReturnType)} {i.Name}({args2})";
        }

        private bool SkipDrawing(Type reflecting, Type declaringType)
        {
            if (reflecting == null || reflecting == declaringType)
                return false;
            while (true)
            {
                if (reflecting.BaseType == null)
                    return false;
                if (_types.ContainsKey(reflecting.BaseType))
                    return true;
                reflecting = reflecting.BaseType;
            }
        }

        public string DiagramName { get; set; }

        private readonly Dictionary<Type, Info2> _types = new Dictionary<Type, Info2>();

        public struct UmlRelationArrow
        {
            public UmlRelationArrow(string leftSign, string rightSign, bool dotted = false)
            {
                LeftSign       = leftSign;
                RightSign      = rightSign;
                Dotted         = dotted;
                ArrowDirection = UmlArrowDirections.Auto;
            }

            public static UmlRelationArrow GetRelationByKind(UmlRelationKind iKind)
            {
                switch (iKind)
                {
                    case UmlRelationKind.AggregationWithArrow:
                        return new UmlRelationArrow("o", ">");
                    case UmlRelationKind.CompositionMany:
                        return new UmlRelationArrow("o", "{");
                    case UmlRelationKind.AggregationDottedWithArrow:
                        return new UmlRelationArrow("o", ">", true);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(iKind), iKind, null);
                }
            }

            public override string ToString()
            {
                var line = Dotted ? "." : "-";
                var sugg = ArrowDirection == UmlArrowDirections.Auto
                    ? ""
                    : ArrowDirection.ToString().ToLower();
                return LeftSign + line + sugg + line + RightSign;
            }

            public UmlRelationArrow With(UmlArrowDirections d)
            {
                ArrowDirection = d;
                return this;
            }

            public static UmlRelationArrow AggregationLeft
            {
                get { return new UmlRelationArrow("o", ">"); }
            }

            public static UmlRelationArrow AggregationLeftMany
            {
                get { return new UmlRelationArrow("o", "{"); }
            }

            public static UmlRelationArrow CompositionLeft
            {
                get { return new UmlRelationArrow("*", ">"); }
            }

            public static UmlRelationArrow CompositionLeftMany
            {
                get { return new UmlRelationArrow("*", "{"); }
            }

            public static UmlRelationArrow InheritLeft
            {
                get { return new UmlRelationArrow("<|", ""); }
            }

            public static UmlRelationArrow InheritRight
            {
                get { return new UmlRelationArrow("", "|>"); }
            }


            public UmlArrowDirections ArrowDirection { get; set; }

            public bool Dotted { get; set; }

            public string LeftSign  { get; set; }
            public string RightSign { get; set; }
        }

        public struct UmlRelationEnd
        {
            public UmlRelationEnd(string name, string label = null)
            {
                Name  = name;
                Label = label;
            }

            public string Left
            {
                get
                {
                    if (string.IsNullOrEmpty(Label))
                        return Name;
                    return Name + " " + Label.AddQuotes();
                }
            }

            public string Right
            {
                get
                {
                    if (string.IsNullOrEmpty(Label))
                        return Name;
                    return Label.AddQuotes() + " " + Name;
                }
            }

            public string Name  { get; }
            public string Label { get; }
        }

        internal class PumlLangInfo : ILangInfo
        {
            public bool AddBOM
            {
                get { return true; }
            }

            public string CloseText
            {
                get { return "}"; }
            }

            public string OneLineComment
            {
                get { return "'"; }
            }

            public string OpenText
            {
                get { return "{"; }
            }
        }

        private class Info2
        {
            public Info2(string name, string bgColor)
            {
                Name    = name;
                BgColor = bgColor;
            }

            public string Name       { get; }
            public string BgColor    { get; }
            public int    OrderIndex { get; set; }
        }

        public class UmlRelation
        {
            public override string ToString()
            {
                var a = Left.Left + " " + Arrow + " " + Right.Right;
                if (string.IsNullOrEmpty(Label))
                    return a;
                return a + ":" + Label.AddQuotes();
            }

            public UmlRelation With(UmlArrowDirections dir)
            {
                Arrow = Arrow.With(dir);
                return this;
            }

            public UmlRelationEnd Left  { get; set; }
            public UmlRelationEnd Right { get; set; }

            public UmlRelationArrow Arrow     { get; set; }
            public string           Label     { get; set; }
            public string           Note      { get; set; }
            public string           NoteColor { get; set; }
        }
    }

    public class CodeWriter
    {
        public void Add(CodeWriter top)
        {
            _sb.AppendLine(top._sb.ToString());
        }

        public void Close()
        {
            _inc--;
            Writeln("}");
        }

        public void Open(string txt)
        {
            Writeln(txt);
            Writeln("{");
            _inc++;
        }

        public void Writeln(string txt)
        {
            if (_inc > 0)
                _sb.AppendLine(new string(' ', _inc * 4) + txt);
            else
                _sb.AppendLine(txt);
        }

        public string Text
        {
            get { return _sb.ToString(); }
        }

        private readonly StringBuilder _sb = new StringBuilder();

        private int _inc;
    }

    public class PlantUmlResult
    {
        public void SaveIfDifferent(string filename)
        {
            var txt = ToString();
            if (File.Exists(filename))
            {
                var existing = File.ReadAllText(filename);
                if (txt == existing)
                    return;
            }

            File.WriteAllText(filename, txt);
        }

        public string ToString()
        {
            var cf = new CodeWriter();
            cf.Writeln("@startuml");
            cf.Add(Top);

            foreach (var t in relations)
            {
                cf.Writeln(t.ToString());
                if (!string.IsNullOrEmpty(t.Note))
                {
                    /*@startuml

class Dummy
Dummy --> Foo : A link
note on link #red: note that is red

Dummy --> Foo2 : Another link
note right on link #blue
this is my note on right link
and in blue
end note

@enduml*/
                    cf.Writeln("note on link " + t.NoteColor);
                    foreach (var line in t.Note.Split('\n', '\r'))
                        if (!string.IsNullOrEmpty(line))
                            cf.Writeln(line);
                    cf.Writeln("end note");
                }
            }

            cf.Writeln("@enduml");

            return cf.Text;
        }

        public CodeWriter                 Top       { get; } = new CodeWriter();
        public List<PlantUml.UmlRelation> relations { get; } = new List<PlantUml.UmlRelation>();
        public CodeWriter                 Classes   { get; } = new CodeWriter();
    }

    public class PlantUmlSkinParams
    {
        /*
      FontColor green
      FontName Verdana
      FontSize 30
      DiamondFontColor red
      DiamondFontSize 40
      ArrowFontSize 7
      */
    }
#endif
}