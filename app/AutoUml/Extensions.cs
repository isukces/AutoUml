using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoUml
{
    public static class Extensions
    {
        public static string AddQuotes(this string x)
        {
            if (x == null)
                return null;
            return "\"" + x + "\"";
        }

        public static string AddQuotesIfNecessary(this string name)
        {
            foreach (var c in name)
                if (!char.IsLetterOrDigit(c))
                    return name.AddQuotes();

            return name;
        }
        
        public static PlantUmlText AsPlantUmlText (this string kind)
        {
            return new PlantUmlText(kind);
        }

        public static PlantUmlText AsPlantUmlText(this OpenIconicKind kind)
        {
            return new PlantUmlText(kind);
        }

        public static string CamelToNormal(this string n, bool onlyFirstUpper)
        {
            n = n?.Trim();
            if (string.IsNullOrEmpty(n))
                return string.Empty;
            var sb = new StringBuilder();
            foreach (var i in n)
            {
                var isUpper    = char.ToUpper(i) == i;
                var isNotFirst = sb.Length > 0;
                if (isUpper && isNotFirst)
                    sb.Append(" ");
                if (isUpper && onlyFirstUpper && isNotFirst)
                    sb.Append(char.ToLower(i));
                else
                    sb.Append(i);
            }

            return sb.ToString();
        }

        public static void DeleteFromListIf<T>(this List<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                return;
            for (var index = list.Count - 1; index >= 0; index--)
            {
                var element = list[index];
                if (predicate(element))
                    list.RemoveAt(index);
            }
        }

        public static string GetDiagramName(this Type type, Func<Type, string> tryGetAlias)
        {
            if (type.IsGenericType)
            {
                var gt = type.GetGenericTypeDefinition();
                if (gt == typeof(Nullable<>))
                    return type.GetGenericArguments()[0].GetDiagramName(tryGetAlias) + "?";
                var name1         = gt.Name.Split('`').First();
                var argCollection = type.GetGenericArguments().Select(a => a.GetDiagramName(tryGetAlias)).ToArray();
                var result        = name1 + "<" + string.Join(",", argCollection) + ">";
                return result;
            }

            if (type.IsArray)
            {
                var qqq = "";
                while (type.IsArray)
                {
                    var rank = type.GetArrayRank();

                    if (rank == 1)
                        qqq += "[]";
                    else
                        qqq += "[" + new string(',', rank - 1) + "]";
                    var el = type.GetElementType();
                    if (el == null)
                        break;
                    type = el;
                }

                qqq = GetDiagramName(type, tryGetAlias) + qqq;
                return qqq;
            }
            //var s = Extensions.TryGetSimpleType

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

            var name = tryGetAlias(type);
            return string.IsNullOrEmpty(name) ? type.Name : name;
        }

        public static Type[] GetGenericTypeArgumentsIfPossible(this Type type)
        {
            if (type == null)
                return new Type[0];
            if (type.IsGenericType)
                return type.GenericTypeArguments;
            return new Type[0];
        }

        public static PropertyInfo[] GetPropertiesInstancePublic(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public static bool IsStruct(this Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
        }

        public static Type MeOrGeneric(this Type type)
        {
            if (type == null)
                return null;
            if (type.IsGenericType)
                return type.GetGenericTypeDefinition();
            return type;
        }

        public static SplittableString MethodToUml(this MethodInfo methodInfo, Func<Type, string> retTypeName)
        {
            var parameters = methodInfo.GetParameters();
            var returnType = retTypeName(methodInfo.ReturnType)+" ";

            if (parameters.Length == 0)
            {
                var i = new[]
                {
                    returnType,
                    methodInfo.Name + "()"
                };
                return SplittableString.Make(i);
            }
            var sink = new Sink<string>(parameters.Length + 2);
            sink.Add(returnType);
            sink.Add(methodInfo.Name + "(");

            var parametersLength = parameters.Length - 1;
            for (var index = 0; index <= parametersLength; index++)
            {
                var parameter = parameters[index];
                var tmp       = retTypeName(parameter.ParameterType) + " " + parameter.Name;
                if (index != parametersLength)
                    tmp += ",";
                else
                    tmp += ")";
                sink.Add(tmp);
            }

            return SplittableString.Make(sink.ToArray());
        }

        public static bool SaveContentIfDifferent(this FileInfo file, string txt)
        {
            var filename = file.FullName;
            if (File.Exists(filename))
            {
                var existing = File.ReadAllText(filename);
                if (txt == existing)
                    return false;
            }

            new FileInfo(filename).Directory?.Create();
            File.WriteAllText(filename, txt);
            return true;
        }


        public static DirectoryInfo SearchFoldersUntilFileExists(this Assembly a, string fileName)
        {
            var di = new FileInfo(a.Location).Directory;
            di = di.SearchFoldersUntilFileExists(fileName);
            return di;
        }

        public static DirectoryInfo SearchFoldersUntilFileExists(this DirectoryInfo di, string fileName)
        {
            while (di != null)
            {
                if (!di.Exists)
                    return null;
                var fi = Path.Combine(di.FullName, fileName);
                if (File.Exists(fi))
                    return di;
                di = di.Parent;
            }

            return null;
        }

        public static string ToInv(this int x)
        {
            return x.ToString(CultureInfo.InvariantCulture);
        }


        public static void Write(this CodeWriter code, string name, int? value)
        {
            if (value != null)
                code.Writeln(name + " " + value.Value.ToInv());
        }


        public static void Write(this CodeWriter code, string name, UmlColor value)
        {
            if (!value.IsEmpty)
                code.Writeln(name + " " + value.PlantUmlCode);
        }

        public static void Write(this CodeWriter code, string name, string value)
        {
            value = value?.Trim();
            if (!string.IsNullOrEmpty(value))
                code.Writeln(name + " " + value);
        }
    }
}