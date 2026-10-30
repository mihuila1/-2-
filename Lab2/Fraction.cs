namespace Lab2;

/// <summary>
/// Задание 5.5. Обыкновенная дробь. Объект неизменяемый: каждая операция возвращает новую дробь.
/// Дробь всегда хранится в несократимом виде, знак находится в числителе.
/// </summary>
public class Fraction
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
        (Numerator, Denominator) = Normalize(numerator, denominator);
    }

    /// <summary>Целое число как дробь n/1.</summary>
    public Fraction(int value) : this(value, 1) { }

    // ----- сложение -----
    public Fraction Sum(Fraction other) =>
        Make((long)Numerator * other.Denominator + (long)other.Numerator * Denominator,
             (long)Denominator * other.Denominator);

    public Fraction Sum(int value) => Sum(new Fraction(value));

    // ----- вычитание -----
    public Fraction Minus(Fraction other) =>
        Make((long)Numerator * other.Denominator - (long)other.Numerator * Denominator,
             (long)Denominator * other.Denominator);

    public Fraction Minus(int value) => Minus(new Fraction(value));

    // ----- умножение -----
    public Fraction Mul(Fraction other) =>
        Make((long)Numerator * other.Numerator,
             (long)Denominator * other.Denominator);

    public Fraction Mul(int value) => Mul(new Fraction(value));

    // ----- деление -----
    public Fraction Div(Fraction other)
    {
        if (other.Numerator == 0)
            throw new DivideByZeroException("Деление на дробь, равную нулю.");
        return Make((long)Numerator * other.Denominator,
                    (long)Denominator * other.Numerator);
    }

    public Fraction Div(int value) => Div(new Fraction(value));

    /// <summary>Строковое представление «числитель/знаменатель».</summary>
    public override string ToString() => $"{Numerator}/{Denominator}";

    private static Fraction Make(long numerator, long denominator)
    {
        (int n, int d) = Normalize(numerator, denominator);
        return new Fraction(n, d);
    }

    private static (int Num, int Den) Normalize(long numerator, long denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException("Знаменатель дроби не может быть равен нулю.");

        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        long gcd = Gcd(Math.Abs(numerator), denominator);
        return (checked((int)(numerator / gcd)), checked((int)(denominator / gcd)));
    }

    private static long Gcd(long a, long b)
    {
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }
}
