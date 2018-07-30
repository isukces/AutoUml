using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoUml
{
    public class UmlProjectDiagram
    {
        public bool ContainsType(Type type)
        {
            return type != null && _entities.ContainsKey(type);
        }

        public void GenerateFile(string filename)
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

            var iter = _entities.OrderBy(a => a.Value.OrderIndex).Select(a => a.Key)
                .ToList();
            var alreadyProcessed = new HashSet<Type>();

            void ProcessList(IEnumerable<Type> typesList)
            {
                foreach (var t in typesList)
                {
                    if (!ContainsType(t))
                        continue;
                    var list = AddToFile(file, t, alreadyProcessed);
                    ProcessList(list);
                }
            }

            ProcessList(iter);
            file.Relations.AddRange(Relations);
            file.SaveIfDifferent(filename);
        }

        public IEnumerable<UmlEntity> GetEntities()
        {
            return _entities.Values;
        }

        public string GetTypeName(Type type)
        {
            return type.GetDiagramName(t =>
            {
                _entities.TryGetValue(type, out var info);
                return info?.Name;
            });
        }

        public void UpdateTypeInfo(Type type, Action<UmlEntity, bool> modification)
        {
            var created = false;
            if (!_entities.TryGetValue(type, out var info))
            {
                created         = true;
                _entities[type] = info = new UmlEntity(type);
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

        private IEnumerable<Type> AddToFile(PlantUmlFile file, Type t,
            HashSet<Type> processed)
        {
            var result = new List<Type>();
            if (!_entities.TryGetValue(t, out _))
                return result;
            if (!processed.Add(t))
                return result;

            var cf = file.Classes;

            if (!_entities.TryGetValue(t, out var info))
                info = new UmlEntity(t);
            cf.Open(info.GetOpenClassCode());
            foreach (var i in info.Members.OrderBy(q => q.Group))
            {
                if (i.HideOnList) continue;
                i.WriteTo(cf, this);
                if (i is PropertyUmlMember propertyUmlMember)
                    result.Add(propertyUmlMember.Property.PropertyType);
            }

            cf.Close();
            return result;
        }


        public UmlDiagramScale   Scale     { get; set; }
        public string            Title     { get; set; }
        public string            Name      { get; set; }
        public UmlSkinParams     Skin      { get; set; } = new UmlSkinParams();
        public List<UmlRelation> Relations { get; set; } = new List<UmlRelation>();
        private readonly Dictionary<Type, UmlEntity> _entities = new Dictionary<Type, UmlEntity>();

        public event EventHandler<AddTypeToDiagramEventArgs> OnAddTypeToDiagram;
    }
}