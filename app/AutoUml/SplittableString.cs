using System;
using System.Collections.Generic;
using System.Text;

namespace AutoUml;

public sealed class SplittableString : IEquatable<SplittableString>
{
    private SplittableString(IReadOnlyList<string> parts)
    {
        Parts = parts;
    }

    public static SplittableString? Make(IReadOnlyList<string>? parts)
    {
        if (parts is null || parts.Count == 0)
            return null;
        return new SplittableString(parts);
    }

    public static SplittableString? Make(params string[] parts)
    {
        return Make((IReadOnlyList<string>)parts);
    }

    public static SplittableString operator +(SplittableString splittable, string? text)
    {
        if (string.IsNullOrEmpty(text))
            return splittable;
        if (splittable is null)
            return new SplittableString([text]);
        var q = new List<string>(splittable.Parts.Count + 1);
        q.AddRange(splittable.Parts);
        q.Add(text);
        return new SplittableString(q.ToArray());
    }

    public static SplittableString operator +(string? text, SplittableString splittable)
    {
        if (string.IsNullOrEmpty(text))
            return splittable;
        if (splittable is null)
            return new SplittableString([text]);
        var q = new List<string>(splittable.Parts.Count + 1) { text };
        q.AddRange(splittable.Parts);
        return new SplittableString(q.ToArray());
    }

    public static bool operator ==(SplittableString? left, SplittableString? right)
    {
        return Equals(left, right);
    }

    public static implicit operator string(SplittableString? x)
    {
        return x?.ToString();
    }

    public static bool operator !=(SplittableString? left, SplittableString? right)
    {
        return !Equals(left, right);
    }

    public bool Equals(SplittableString? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (Parts.Count != other.Parts.Count)
            return false;
        for (var i = Parts.Count - 1; i >= 0; i--)
            if (Parts[i] != other.Parts[i])
                return false;

        return true;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is SplittableString other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Parts.Count;
    }

    public int MakeAction(int maxLineLength, Action<int, string> flushLine)
    {
        var lineIndex = 0;
        var sink      = new StringBuilder();
        for (var index = 0; index < Parts.Count; index++)
        {
            var part   = Parts[index];
            var length = sink.Length;
            if (length > 0 && length + part.Length > maxLineLength)
            {
                flushLine(lineIndex++, sink.ToString());
                sink.Clear();
            }

            sink.Append(part);
        }

        if (sink.Length > 0)
            flushLine(lineIndex++, sink.ToString());
        return lineIndex;
    }

    public override string ToString()
    {
        return string.Join("", Parts);
    }

    public IReadOnlyList<string> Parts { get; }
}
