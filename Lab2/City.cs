namespace Lab2;

/// <summary>Путь в другой город: город назначения и стоимость поездки.</summary>
public class Route
{
    public City Destination { get; }
    public int Cost { get; }

    public Route(City destination, int cost)
    {
        Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        if (cost <= 0)
            throw new ArgumentOutOfRangeException(nameof(cost), "Стоимость поездки должна быть положительной.");
        Cost = cost;
    }

    /// <summary>Пара «название:стоимость».</summary>
    public override string ToString() => $"{Destination.Name}:{Cost}";
}

/// <summary>
/// Задания 3.3 и 4.8. Город — точка на карте с названием и набором путей в другие города.
/// </summary>
public class City
{
    private readonly List<Route> _routes = new();

    public string Name { get; }

    public IReadOnlyList<Route> Routes => _routes;

    /// <summary>Задание 4.8: город можно создать, указав только название.</summary>
    public City(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название города не может быть пустым.", nameof(name));
        Name = name.Trim();
    }

    /// <summary>Задание 4.8: город можно создать, указав название и набор путей.</summary>
    public City(string name, IEnumerable<Route> routes) : this(name)
    {
        ArgumentNullException.ThrowIfNull(routes);
        foreach (Route route in routes)
            AddRoute(route.Destination, route.Cost);
    }

    /// <summary>Добавить односторонний путь. Если путь в этот город уже есть, стоимость обновляется.</summary>
    public void AddRoute(City destination, int cost)
    {
        ArgumentNullException.ThrowIfNull(destination);
        if (ReferenceEquals(destination, this))
            throw new ArgumentException("Нельзя построить путь из города в него самого.", nameof(destination));

        _routes.RemoveAll(r => ReferenceEquals(r.Destination, destination));
        _routes.Add(new Route(destination, cost));
    }

    /// <summary>Добавить двусторонний путь с одинаковой стоимостью в обе стороны.</summary>
    public static void Connect(City a, City b, int cost)
    {
        a.AddRoute(b, cost);
        b.AddRoute(a, cost);
    }

    /// <summary>Название города и список связанных городов в виде пар «название:стоимость».</summary>
    public override string ToString() =>
        _routes.Count == 0
            ? $"{Name} -> (путей нет)"
            : $"{Name} -> {string.Join(", ", _routes)}";
}
