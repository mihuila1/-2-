using System.Globalization;

namespace Lab2;

/// <summary>Задание 1.1. Точка на двумерной плоскости.</summary>
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point() : this(0, 0) { }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Текстовое представление вида «{X;Y}».</summary>
    public override string ToString() =>
        string.Create(CultureInfo.InvariantCulture, $"{{{X};{Y}}}");
}
