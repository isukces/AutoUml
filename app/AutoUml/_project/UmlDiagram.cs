using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AutoUml
{
    public class UmlDiagram : IMetadataContainer
    {
        public bool ContainsType(Type type)
        {
            return type != null && _entities.ContainsKey(type);
        }

        public PlantUmlFile CreateFile()
        {
            var file = new PlantUmlFile();
            Skin?.WriteTo(file.Top);
            if (!Scale.IsEmpty)
                file.Top.Writeln("scale " + Scale);
            if (!string.IsNullOrEmpty(Title))
            {
                file.Top.Writeln("title");
                file.Top.Writeln(' ' + Title);
                file.Top.Writeln("end title");
            }

            // sprites
            foreach (var i in Sprites.OrderBy(a => a.Key))
                i.Value.Save(file, i.Key);
            var alreadyProcessed = new HashSet<Type>();

            bool isPackageOpen = false;

            void ClosePackage()
            {
                if (isPackageOpen)
                    file.Classes.Close();
                isPackageOpen = false;
            }

            void OpenPackage(string pn, string kind)
            {
                ClosePackage();
                if (string.IsNullOrEmpty(pn))
                    return;
                file.Classes.OpenSameLine("package " + pn.AddQuotesIfNecessary() + " <<" + kind + ">>");
                isPackageOpen = true;
            }

            var usedPackages = new Dictionary<string, UmlPackage>(StringComparer.CurrentCultureIgnoreCase);
            int typesToDo    = 0;

            void ProcessList(IEnumerable<Type> typesList, string currentPackageName, bool usePackageName)
            {
                foreach (var t in typesList.Select(a=>a.MeOrGeneric()))
                {
                    if (!ContainsType(t))
                        continue;
                    if (alreadyProcessed.Contains(t))
                        continue;
                    if (!_entities.TryGetValue(t, out var entity))
                        continue;
                    var entityPackageName = entity.PackageName?.Trim() ?? string.Empty;
                    if (IgnorePackages)
                        entityPackageName = string.Empty;
                    if (!usePackageName)
                    {
                        usePackageName     = true;
                        currentPackageName = entityPackageName;
                        if (!Packages.TryGetValue(currentPackageName, out var package))
                            package = new UmlPackage();
                        usedPackages[currentPackageName] = package;
                        OpenPackage(currentPackageName, package.Kind.ToString());
                    }

                    if (!string.Equals(currentPackageName, entityPackageName, StringComparison.OrdinalIgnoreCase))
                        continue;
                    alreadyProcessed.Add(t);
                    var list = AddToFile(file, t);
                    typesToDo--;
                    ProcessList(list, currentPackageName, true);
                }
            }

            var types = _entities.OrderBy(a => a.Value.OrderIndex).Select(a => a.Key)
                .ToList();
            typesToDo = types.Count;
            while (typesToDo > 0)
            {
                ProcessList(types, null, false);
            }
            ClosePackage();

            file.Relations.AddRange(Relations);
            return file;
        }

        public IEnumerable<UmlEntity> GetEntities()
        {
            return _entities.Values;
        }


        public string GetTypeName(Type type)
        {
            return type.GetDiagramName(t => TryGetEntityByType(t)?.Name);
        }

        public bool SaveToFile(string filename)
        {
            var file = CreateFile();
            return file.SaveIfDifferent(filename);
        }

        [CanBeNull]
        public UmlEntity TryGetEntityByType(Type type)
        {
            if (type == null)
                return null;
            return _entities.TryGetValue(type, out var ent) ? ent : null;
        }
         

        public void UpdateTypeInfo(Type type, [CanBeNull] Action<UmlEntity, bool> modification)
        {
            var created = false;
            if (!_entities.TryGetValue(type, out var info))
            {
                created         = true;
                _entities[type] = info = new UmlEntity(type, GetTypeName);
            }

            if (modification != null)
                modification(info, created);
            if (!created) return;
            var handler = OnAddTypeToDiagram;
            if (handler == null) return;
            var args = new AddTypeToDiagramEventArgs
            {
                Info    = info,
                Diagram = this
            };
            handler.Invoke(this, args);
        }

        private IEnumerable<Type> AddToFile(PlantUmlFile file, Type t)
        {
            var result = new List<Type>();
            var cf     = file.Classes;
            if (!_entities.TryGetValue(t, out var info))
                info = new UmlEntity(t, GetTypeName);
            cf.Open(info.GetOpenClassCode());

            foreach (var i in info.Members.OrderBy(q => q.Group))
            {
                if (i.HideOnList) continue;
                i.WriteTo(cf, this);
                if (i is PropertyUmlMember propertyUmlMember)
                    result.Add(propertyUmlMember.Property.PropertyType);
            }

            cf.Close();
            foreach (var i in info.Notes.OrderBy(a => a.Key))
            {
                var bg = i.Value.Background?.GetCode();
                if (!string.IsNullOrEmpty(bg))
                    bg = " " + bg;
                cf.Writeln($"note {i.Key.ToString().ToLower()} of {info.Name.AddQuotesIfNecessary()}{bg}");
                foreach (var j in i.Value.Text.Split('\n'))
                    if (!string.IsNullOrEmpty(j))
                        cf.Writeln(j);
                cf.Writeln("end note");
            }

            return result;
        }

        public UmlDiagramScale               Scale     { get; set; }
        public string                        Title     { get; set; }
        public string                        Name      { get; set; }
        public UmlSkinParams                 Skin      { get; set; } = new UmlSkinParams();
        public List<UmlRelation>             Relations { get; set; } = new List<UmlRelation>();
        public Dictionary<string, object>    Metadata  { get; }      = new Dictionary<string, object>();
        public Dictionary<string, UmlSprite> Sprites   { get; }      = new Dictionary<string, UmlSprite>();
        public bool IgnorePackages { get; set; }

        public Dictionary<string, UmlPackage> Packages { get; } =
            new Dictionary<string, UmlPackage>(StringComparer.CurrentCultureIgnoreCase);

        private readonly Dictionary<Type, UmlEntity> _entities = new Dictionary<Type, UmlEntity>();

        public event EventHandler<AddTypeToDiagramEventArgs> OnAddTypeToDiagram;

    }
}