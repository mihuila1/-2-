namespace Lab2;

/// <summary>Задание 2.1. Линия на плоскости, заданная двумя точками.</summary>
public class Line
{
    private Point _start;
    private Point _end;

    /// <summary>Начало линии. Точка хранится по ссылке, поэтому её могут разделять несколько линий.</summary>
    public Point Start
    {
        get => _start;
        set => _start = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>Конец линии (также хранится по ссылке).</summary>
    public Point End
    {
        get => _end;
        set => _end = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Line(Point start, Point end)
    {
        _start = start ?? throw new ArgumentNullException(nameof(start));
        _end = end ?? throw new ArgumentNullException(nameof(end));
    }

    public Line(double x1, double y1, double x2, double y2)
        : this(new Point(x1, y1), new Point(x2, y2)) { }

    /// <summary>Текстовое представление вида «Линия от {X1;Y1} до {X2;Y2}».</summary>
    public override string ToString() => $"Линия от {Start} до {End}";
}
