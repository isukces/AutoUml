using System;

namespace AutoUml;

public struct GradientColorFill : IUmlFill
{
    public GradientColorFill(UmlColor first, UmlColor second, GradientDirection direction)
    {
        _first     = first;
        _second    = second;
        _direction = direction;
    }

    public string GetCode(bool convertToRgb)
    {
        var c1 = convertToRgb ? _first.PlantUmlRgbCode : _first.PlantUmlCode;
        var c2 = convertToRgb ? _second.PlantUmlRgbCode : _second.PlantUmlCode;
        if (_first.IsEmpty)
            return c2;
        if (_second.IsEmpty)
            return c1;
        if (c1 == c2)
            return c1;
        c1 = c1.Substring(1);
        c2 = c2.Substring(1);
        switch (_direction)
        {
            case GradientDirection.Up:
                return $"#{c2}-{c1}";
            case GradientDirection.Down:
                return $"#{c1}-{c2}";
            case GradientDirection.Left:
                return $"#{c2}|{c1}";
            case GradientDirection.Right:
                return $"#{c1}|{c2}";
            case GradientDirection.UpLeft:
                return $"#{c2}/{c1}";
            case GradientDirection.UpRight:
                return $"#{c1}\\{c2}";
            case GradientDirection.DownLeft:
                return $"#{c2}\\{c1}";
            case GradientDirection.DownRight:
                return $"#{c1}/{c2}";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private readonly UmlColor _first;
    private readonly UmlColor _second;
    private readonly GradientDirection _direction;
}

public enum GradientDirection
{
    Up,
    Down,
    Left,
    Right,


    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
