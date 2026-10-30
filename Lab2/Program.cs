using System.Globalization;
using System.Text;

namespace Lab2;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Лабораторная работа 2, вариант 5 ===");
            Console.WriteLine("1. Задание 1.1 — Точка");
            Console.WriteLine("2. Задание 1.3 — Имена");
            Console.WriteLine("3. Задание 2.1 — Линия");
            Console.WriteLine("4. Задание 3.3 — Города (схема с рисунка 2)");
            Console.WriteLine("5. Задание 4.8 — Создаём города");
            Console.WriteLine("6. Задание 5.5 — Дроби (примеры)");
            Console.WriteLine("7. Задание 5.5 — Калькулятор дробей (ввод с клавиатуры)");
            Console.WriteLine("0. Выход");

            int choice = ReadInt("Выберите пункт меню: ", 0, 7);
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case 0: return;
                    case 1: DemoPoints(); break;
                    case 2: DemoNames(); break;
                    case 3: DemoLines(); break;
                    case 4: DemoCityScheme(); break;
                    case 5: DemoCityConstructors(); break;
                    case 6: DemoFractions(); break;
                    case 7: DemoFractionCalculator(); break;
                }
            }
            catch (Exception ex) when (ex is DivideByZeroException or OverflowException or ArgumentException)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    // ---------------------------------------------------------------- задание 1.1
    private static void DemoPoints()
    {
        Console.WriteLine("--- Задание 1.1. Три точки ---");
        var points = new[] { new Point(3, 5), new Point(25, 6), new Point(-7.5, 8) };
        for (int i = 0; i < points.Length; i++)
            Console.WriteLine($"Точка {i + 1}: {points[i]}");

        Console.WriteLine();
        Console.WriteLine("Создайте свою точку:");
        double x = ReadDouble("  X = ");
        double y = ReadDouble("  Y = ");
        Console.WriteLine($"Ваша точка: {new Point(x, y)}");
    }

    // ---------------------------------------------------------------- задание 1.3
    private static void DemoNames()
    {
        Console.WriteLine("--- Задание 1.3. Имена ---");
        var names = new[]
        {
            new Name(firstName: "Клеопатра"),
            new Name("Пушкин", "Александр", "Сергеевич"),
            new Name("Маяковский", "Владимир"),
        };
        foreach (Name name in names)
            Console.WriteLine(name);

        Console.WriteLine();
        Console.WriteLine("Введите своё имя (любую часть можно пропустить, нажав Enter):");
        Console.Write("  Фамилия: ");
        string? last = Console.ReadLine();
        Console.Write("  Имя: ");
        string? first = Console.ReadLine();
        Console.Write("  Отчество: ");
        string? middle = Console.ReadLine();

        string result = new Name(last, first, middle).ToString();
        Console.WriteLine(result.Length == 0 ? "Вы не ввели ни одной части имени." : $"Результат: {result}");
    }

    // ---------------------------------------------------------------- задание 2.1
    private static void DemoLines()
    {
        Console.WriteLine("--- Задание 2.1. Линии ---");

        // 1. Линия 1 с началом в {1;3} и концом в {23;8}
        var line1 = new Line(new Point(1, 3), new Point(23, 8));
        // 2. Линия 2 горизонтальная, на высоте 10, от точки 5 до точки 25
        var line2 = new Line(new Point(5, 10), new Point(25, 10));
        // 3. Линия 3 использует ТЕ ЖЕ объекты-точки: начало линии 1 и конец линии 2
        var line3 = new Line(line1.Start, line2.End);

        Console.WriteLine("После создания:");
        PrintLines(line1, line2, line3);

        // 4. Меняем координаты самих точек (объекты остаются теми же) —
        //    линия 3 следует за изменениями автоматически.
        line1.Start.X = 2;
        line1.Start.Y = 4;
        line1.End.X = 30;
        line2.Start.Y = 12;
        line2.End.X = 40;
        line2.End.Y = 12;

        Console.WriteLine();
        Console.WriteLine("После изменения координат точек (линия 3 изменилась вместе с линиями 1 и 2):");
        PrintLines(line1, line2, line3);

        // 5. Чтобы линия 3 НЕ изменилась, начало линии 1 заменяем новым объектом-точкой,
        //    а не меняем координаты старого. Конец линии 1 линии 3 не принадлежит,
        //    поэтому его можно менять свободно.
        line1.Start = new Point(100, 100);
        line1.End.X = 50;

        Console.WriteLine();
        Console.WriteLine("После замены начала линии 1 (линия 3 осталась прежней):");
        PrintLines(line1, line2, line3);
    }

    private static void PrintLines(Line line1, Line line2, Line line3)
    {
        Console.WriteLine($"  Линия 1: {line1}");
        Console.WriteLine($"  Линия 2: {line2}");
        Console.WriteLine($"  Линия 3: {line3}");
    }

    // ---------------------------------------------------------------- задание 3.3
    private static void DemoCityScheme()
    {
        Console.WriteLine("--- Задание 3.3. Схема городов (рисунок 2) ---");

        var a = new City("A");
        var b = new City("B");
        var c = new City("C");
        var d = new City("D");
        var e = new City("E");
        var f = new City("F");

        City.Connect(a, b, 5);
        City.Connect(a, f, 1);
        f.AddRoute(b, 1);          // путь F -> B односторонний
        City.Connect(f, e, 2);
        City.Connect(b, c, 3);
        City.Connect(c, d, 4);
        City.Connect(d, e, 2);
        City.Connect(a, d, 6);

        foreach (City city in new[] { a, b, c, d, e, f })
            Console.WriteLine(city);
    }

    // ---------------------------------------------------------------- задание 4.8
    private static void DemoCityConstructors()
    {
        Console.WriteLine("--- Задание 4.8. Создаём города ---");

        var moscow = new City("Москва");                       // только название
        var tula = new City("Тула");                           // только название
        var kaluga = new City("Калуга", new[]                  // название + набор путей
        {
            new Route(moscow, 180),
            new Route(tula, 90),
        });

        Console.WriteLine(moscow);
        Console.WriteLine(tula);
        Console.WriteLine(kaluga);
    }

    // ---------------------------------------------------------------- задание 5.5
    private static void DemoFractions()
    {
        Console.WriteLine("--- Задание 5.5. Дроби ---");

        var f1 = new Fraction(1, 3);
        var f2 = new Fraction(2, 3);
        var f3 = new Fraction(3, 4);

        Console.WriteLine($"f1 = {f1}, f2 = {f2}, f3 = {f3}");
        Console.WriteLine();

        Console.WriteLine($"{f1} + {f2} = {f1.Sum(f2)}");
        Console.WriteLine($"{f1} + 2 = {f1.Sum(2)}");
        Console.WriteLine($"{f3} - {f1} = {f3.Minus(f1)}");
        Console.WriteLine($"{f3} - 1 = {f3.Minus(1)}");
        Console.WriteLine($"{f1} * {f2} = {f1.Mul(f2)}");
        Console.WriteLine($"{f3} * 4 = {f3.Mul(4)}");
        Console.WriteLine($"{f1} / {f3} = {f1.Div(f3)}");
        Console.WriteLine($"{f2} / 2 = {f2.Div(2)}");

        Console.WriteLine();
        Console.WriteLine($"Исходные дроби не изменились: f1 = {f1}, f2 = {f2}, f3 = {f3}");

        Fraction result = f1.Sum(f2).Div(f3).Minus(5);
        Console.WriteLine($"f1.Sum(f2).Div(f3).Minus(5) = {result}");
    }

    private static void DemoFractionCalculator()
    {
        Console.WriteLine("--- Задание 5.5. Калькулятор дробей ---");

        Fraction first = ReadFraction("Первая дробь:");

        Console.WriteLine("Операция: 1 — сложение, 2 — вычитание, 3 — умножение, 4 — деление");
        int op = ReadInt("Ваш выбор: ", 1, 4);

        Console.WriteLine("Второй операнд: 1 — дробь, 2 — целое число");
        int kind = ReadInt("Ваш выбор: ", 1, 2);

        Fraction second = kind == 1 ? ReadFraction("Вторая дробь:") : new Fraction(ReadInt("Целое число: "));

        (string symbol, Fraction result) = op switch
        {
            1 => ("+", first.Sum(second)),
            2 => ("-", first.Minus(second)),
            3 => ("*", first.Mul(second)),
            _ => ("/", first.Div(second)),
        };

        Console.WriteLine($"{first} {symbol} {second} = {result}");
    }

    private static Fraction ReadFraction(string title)
    {
        Console.WriteLine(title);
        int numerator = ReadInt("  Числитель: ");

        int denominator;
        while (true)
        {
            denominator = ReadInt("  Знаменатель: ");
            if (denominator != 0)
                break;
            Console.WriteLine("Ошибка: знаменатель не может быть равен нулю.");
        }

        return new Fraction(numerator, denominator);
    }

    // ---------------------------------------------------------------- ввод с проверкой
    private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (input is null)
                throw new InvalidOperationException("Ввод прерван.");

            if (int.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)
                && value >= min && value <= max)
                return value;

            Console.WriteLine(min == int.MinValue && max == int.MaxValue
                ? "Ошибка: введите целое число."
                : $"Ошибка: введите целое число от {min} до {max}.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (input is null)
                throw new InvalidOperationException("Ввод прерван.");

            // Принимаем и точку, и запятую как разделитель дробной части.
            string normalized = input.Trim().Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                && double.IsFinite(value))
                return value;

            Console.WriteLine("Ошибка: введите число (например, 5 или -2.5).");
        }
    }
}
