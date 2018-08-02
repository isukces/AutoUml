using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public class UmlEntity : ICustomDataContainer
    {
        public UmlEntity([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = type.Name;
            if (type.IsInterface)
                KeyWord = UmlTypes.UmlInterface;
            else if (type.IsEnum)
                KeyWord = UmlTypes.UmlEnum;
            else
                KeyWord = UmlTypes.UmlClass;
            if (type.IsAbstract)
                IsAbstract = true;
        }

        public void AddNote(INoteWithLocationProvider np)
        {
            if (np == null)
                return;
            AddNote(np.GetNoteLocation(), np.GetNoteText(), np.GetNoteBackground());
        }

        public void AddNote(NoteLocation location, string note, IUmlFill background = null)
        {
            note = note?.Trim();
            if (string.IsNullOrEmpty(note))
                return;
            if (!_notes.TryGetValue(location, out var x))
                _notes[location] = x = new UmlNote();
            if (background != null)
                x.Background = background;
            x.Text += "\n" + note;
        }

        public string GetOpenClassCode()
        {
            var items = new List<string>();
            if (IsAbstract && KeyWord == UmlTypes.UmlClass)
                items.Add("abstract");
            items.Add(KeyWord.ToString().ToLower().Substring(3));
            items.Add(Name.AddQuotesIfNecessary());
            var spot = Spot?.PlantUmlCode;
            if (!string.IsNullOrEmpty(spot))
                items.Add(spot);
            {
                var code = Background?.GetCode();
                if (!string.IsNullOrEmpty(code))
                    items.Add(code);
            }
            return string.Join(" ", items);
        }


        [NotNull]
        public Type Type { get; }

        public string          Name       { get; set; }
        public IUmlFill        Background { get; set; }
        public int             OrderIndex { get; set; }
        public UmlSpot         Spot       { get; set; }
        public UmlTypes        KeyWord    { get; set; }
        public bool            IsAbstract { get; set; }
        public List<UmlMember> Members    { get; set; } = new List<UmlMember>();

        public IReadOnlyDictionary<NoteLocation, UmlNote> Notes
        {
            get { return _notes; }
        }

        public Dictionary<string, object> CustomData { get; } = new Dictionary<string, object>();

        private readonly Dictionary<NoteLocation, UmlNote> _notes = new Dictionary<NoteLocation, UmlNote>();
    }

    public enum UmlTypes
    {
        UmlClass,
        UmlInterface,
        UmlEnum
    }

    public enum NoteLocation
    {
        Top,
        Bottom,
        Left,
        Right
    }
}