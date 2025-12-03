using System;

namespace AutoUml;

public struct UmlPackageName : IEquatable<UmlPackageName>, IComparable<UmlPackageName>, IComparable
{
    public UmlPackageName(string name)
    {
        _name = name?.Trim();
    }

    public static bool operator ==(UmlPackageName left, UmlPackageName right)
    {
        return left.Equals(right);
    }

    public static bool operator >(UmlPackageName left, UmlPackageName right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(UmlPackageName left, UmlPackageName right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator !=(UmlPackageName left, UmlPackageName right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(UmlPackageName left, UmlPackageName right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(UmlPackageName left, UmlPackageName right)
    {
        return left.CompareTo(right) <= 0;
    }

    public int CompareTo(UmlPackageName other)
    {
        return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public int CompareTo(object obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        return obj is UmlPackageName other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(UmlPackageName)}");
    }

    public bool Equals(UmlPackageName other)
    {
        return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object obj)
    {
        return obj is UmlPackageName other && Equals(other);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
    }

    public override string ToString()
    {
        return Name;
    }

    public static UmlPackageName Empty => new(null);

    public string Name => _name ?? string.Empty;

    public bool IsEmpty => string.IsNullOrEmpty(_name);

    private readonly string _name;
}
