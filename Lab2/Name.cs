namespace Lab2;

/// <summary>Задание 1.3. Имя: фамилия, личное имя, отчество (любая часть может быть не задана).</summary>
public class Name
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }

    public Name(string? lastName = null, string? firstName = null, string? middleName = null)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    /// <summary>«Фамилия Имя Отчество»; не заданные части в строку не попадают.</summary>
    public override string ToString() =>
        string.Join(" ", new[] { LastName, FirstName, MiddleName }
            .Where(part => !string.IsNullOrWhiteSpace(part))
            .Select(part => part!.Trim()));
}
