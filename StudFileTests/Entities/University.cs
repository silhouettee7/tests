namespace StudFileTests.Entities;

public class University(string name, string abbreviation, string city)
{
    public string Name { get;  } = name;
    public string Abbreviation { get;  } = abbreviation;
    public string City { get; } = city;
}