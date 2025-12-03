using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AutoUml;

public class UmlDiagram : IMetadataContainer
{
    public bool ContainsType(Type? type)
    {
        return type != null && _entities.ContainsKey(type);
    }

    public PlantUmlFile CreateFile()
    {
        _state = new CreationState();

        Skin?.WriteTo(_state.File.Top);
        if (!Scale.IsEmpty)
            _state.File.Top.Writeln("scale " + Scale);

        var hm = HideMembers.GetPumlCommands("hide");
        foreach(var i in hm)
            _state.File.Top.Writeln(i);

        if (!string.IsNullOrEmpty(Title))
        {
            _state.File.Top.Writeln("title");
            _state.File.Top.Writeln(' ' + Title);
            _state.File.Top.Writeln("end title");
        }

        // sprites
        foreach (var i in Sprites.OrderBy(a => a.Key))
            i.Value.Save(_state.File, i.Key);

        var types = _entities.OrderBy(a => a.Value.OrderIndex)
            .Select(a => a.Key)
            .ToList();
        if (IgnorePackages)
        {
            ProcessList(types, UmlPackageName.Empty);
        }
        else
        {
            var pcks = _entities.Values.Select(GetPackageName)
                .Distinct()
                .OrderBy(a => a).ToArray();
            foreach (var i in pcks) ProcessList(types, i);
        }

        PackageClose();

        _state.File.Relations.AddRange(Relations);

        Legend.WriteTo(_state.File.Classes);

        var result = _state.File;
        _state = null;
        return result;
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

    public UmlEntity? TryGetEntityByType(Type? type)
    {
        if (type == null)
            return null;
        return _entities.TryGetValue(type, out var ent) ? ent : null;
    }


    public void UpdateTypeInfo(Type type, Action<UmlEntity, bool>? modification)
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

    private void AddToFile(Type type)
    {
        var cf = _state.File.Classes;
        if (!_entities.TryGetValue(type, out var info))
            info = new UmlEntity(type, GetTypeName);
        cf.Open(info.GetOpenClassCode());

        {
            var l = info.StartingLines?.SplitLines(true);
            if (l != null)
                foreach (var i in l)
                    cf.Writeln(i);
        }
        foreach (var i in info.Members.OrderBy(q => q.Group))
        {
            if (i.HideOnList) continue;
            i.WriteTo(cf, this);
        }

        cf.Close();
        var notes = info.Notes.OrderBy(a => a.Key);
        foreach (var i in notes)
        {
            var text  = i.Value.Text;
            var lines = text.SplitLines(true);
            if (lines is null)
                continue;

            var bg = i.Value.Background?.GetCode();
            if (!string.IsNullOrEmpty(bg))
                bg = " " + bg;
            var openNote = $"note {i.Key.ToString().ToLower()} of {info.Name.AddQuotesIfNecessary()}{bg}";
            cf.Writeln(openNote);
            foreach (var j in lines)
                cf.Writeln(j);
            cf.Writeln("end note");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetOrClearHideMembers(MembersToHide flag, bool set)
    {
        if (set)
            HideMembers |= flag;
        else
            HideMembers &= ~flag;
    }

    private UmlPackageName GetPackageName(UmlEntity entity)
    {
        return IgnorePackages ? UmlPackageName.Empty : new UmlPackageName(entity.PackageName);
    }


    private void PackageClose()
    {
        if (!_state.IsPackageOpen) return;
        _state.File.Classes.Close();
        _state.PackageName = UmlPackageName.Empty;
    }


    private void PackageOpen(UmlPackageName pn, string kind)
    {
        if (_state.PackageName == pn)
            return;
        PackageClose();
        if (pn.IsEmpty)
            return;

        var text = $"package {pn.Name.AddQuotesIfNecessary()} <<{kind}>>";
        _state.File.Classes.OpenSameLine(text);
        _state.PackageName = pn;
    }


    private void ProcessList(IEnumerable<Type> types, UmlPackageName usePackageOnly)
    {
        foreach (var srcType in types)
        {
            var type = srcType.MeOrGeneric();
            if (!ContainsType(type))
                continue;
            if (_state.ProcessedTypes.Contains(type) || _state.ProcessedTypes.Contains(srcType))
                continue;
            if (!_entities.TryGetValue(type, out var entity))
                continue;
            var entityPackageName = GetPackageName(entity);
            if (usePackageOnly != entityPackageName)
                continue;
            if (entityPackageName.IsEmpty)
            {
                PackageClose();
            }
            else
            {
                if (_state.IsPackageOpen && _state.PackageName == entityPackageName)
                {
                }
                else
                {
                    if (!Packages.TryGetValue(entityPackageName, out var package))
                        package = new UmlPackage();
                    _state.UsedPackages[entityPackageName] = package;
                    PackageOpen(entityPackageName, package.Kind.ToString());
                    // OpenPackage(entityPackageName, );
                }
            }

            _state.ProcessedTypes.Add(type);
            _state.ProcessedTypes.Add(srcType);
            AddToFile(type);
        }
    }

    public UmlDiagramLegend Legend { get; } = new UmlDiagramLegend();

    public UmlDiagramScale               Scale          { get; set; }
    public string                        Title          { get; set; }
    public string                        Name           { get; set; }
    public UmlSkinParams                 Skin           { get; set; } = new UmlSkinParams();
    public List<UmlRelation>             Relations      { get; set; } = new List<UmlRelation>();
    public Dictionary<string, object>    Metadata       { get; }      = new Dictionary<string, object>();
    public Dictionary<string, UmlSprite> Sprites        { get; }      = new Dictionary<string, UmlSprite>();
    public bool                          IgnorePackages { get; set; }

    [Obsolete("use " + nameof(HideMembers) + " instead")]
    public bool HideEmptyMethods
    {
        get => (HideMembers & MembersToHide.EmptyMethods) == MembersToHide.EmptyMethods;
        set => SetOrClearHideMembers(MembersToHide.EmptyMethods, value);
    }

    [Obsolete("use " + nameof(HideMembers) + " instead")]
    public bool HideEmptyAttributes
    {
        get => (HideMembers & MembersToHide.EmptyAttributes) == MembersToHide.EmptyAttributes;
        set => SetOrClearHideMembers(MembersToHide.EmptyAttributes, value);
    }


    public MembersToHide HideMembers { get; set; }

    public Dictionary<UmlPackageName, UmlPackage> Packages { get; } =
        new Dictionary<UmlPackageName, UmlPackage>();

    private readonly Dictionary<Type, UmlEntity> _entities = new Dictionary<Type, UmlEntity>();

    private CreationState _state;

    public event EventHandler<AddTypeToDiagramEventArgs> OnAddTypeToDiagram;


    private sealed class CreationState
    {
        public PlantUmlFile File { get; } = new PlantUmlFile();

        public Dictionary<UmlPackageName, UmlPackage> UsedPackages { get; } =
            new Dictionary<UmlPackageName, UmlPackage>();

        public HashSet<Type> ProcessedTypes { get; } = new HashSet<Type>();

        public bool IsPackageOpen => !PackageName.IsEmpty;

        public UmlPackageName PackageName { get; set; }
    }
}