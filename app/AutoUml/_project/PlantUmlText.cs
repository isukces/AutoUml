using System;

namespace AutoUml
{
    public sealed class PlantUmlText : IEquatable<PlantUmlText>, IComparable<PlantUmlText>, IComparable
    {
        public PlantUmlText(string text)
        {
            Text = text;
        }

        public PlantUmlText(OpenIconicKind kind)
        {
            Text = "<&" + kind.ToCode() + ">";

        }

        public static bool operator ==(PlantUmlText left, PlantUmlText? right)
        {
            return left.Equals(right);
        }

        public static bool operator >(PlantUmlText left, PlantUmlText right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(PlantUmlText left, PlantUmlText right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static implicit operator PlantUmlText(string x)
        {
            return new PlantUmlText(x);
        }

        public static implicit operator PlantUmlText(OpenIconicKind x)
        {
            return new PlantUmlText(x);
        }

        public static bool operator !=(PlantUmlText left, PlantUmlText? right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(PlantUmlText left, PlantUmlText right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(PlantUmlText left, PlantUmlText right)
        {
            return left.CompareTo(right) <= 0;
        }

        public int CompareTo(PlantUmlText other)
        {
            return string.Compare(Text, other.Text, StringComparison.Ordinal);
        }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is PlantUmlText other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(PlantUmlText)}");
        }

        public bool Equals(PlantUmlText? other)
        {
            return Text == other.Text;
        }

        public override bool Equals(object? obj)
        {
            return obj is PlantUmlText other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return Text;
        }

        public string Text { get; }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(Text); }
        }
    }
}