namespace StudFileTests.Entities;

public class Subject(string name, string abbreviation, string parentSubjectName)
{
    public string Name { get; } = name;
    public string Abbreviation { get; } = abbreviation;
    public string ParentSubjectName { get; } = parentSubjectName;
}