using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AutoUml
{
    public class UmlEntity
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

        public void AddNote(NoteLocation location, string note)
        {
            note = note?.Trim();
            if (string.IsNullOrEmpty(note))
                return;
            if (_notes.TryGetValue(location, out var currentText))
                note = currentText + "\n" + note;
            _notes[location] = note;
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
            if (!BgColor.IsEmpty)
                items.Add(BgColor.PlantUmlCode);
            return string.Join(" ", items);
        }


        [NotNull]
        public Type Type { get; }

        public string          Name       { get; set; }
        public UmlColor        BgColor    { get; set; }
        public int             OrderIndex { get; set; }
        public UmlSpot         Spot       { get; set; }
        public UmlTypes        KeyWord    { get; set; }
        public bool            IsAbstract { get; set; }
        public List<UmlMember> Members    { get; set; } = new List<UmlMember>();

        private readonly Dictionary<NoteLocation, string> _notes = new Dictionary<NoteLocation, string>();
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