using System.Xml.Linq;
using StudFileTests.Entities;

namespace StudFileTests.TestData;

public class TestDataLoader
{
    private readonly XDocument _document;
    private readonly Random _random = new();

    public TestDataLoader(string xmlFilePath)
    {
        if (!File.Exists(xmlFilePath))
        {
            throw new FileNotFoundException($"Файл тестовых данных не найден: {xmlFilePath}");
        }

        _document = XDocument.Load(xmlFilePath);
    }

    public IEnumerable<University> LoadUniversities()
    {
        var universities = _document.Root?
            .Element("Universities")?
            .Elements("University")
            .Select(x => new University(
                x.Element("Name")?.Value ?? string.Empty,
                x.Element("Abbreviation")?.Value ?? string.Empty,
                x.Element("City")?.Value ?? string.Empty
            )) ?? Enumerable.Empty<University>();

        return universities;
    }

    public IEnumerable<Subject> LoadSubjects()
    {
        var subjects = _document.Root?
            .Element("Subjects")?
            .Elements("Subject")
            .Select(x => new Subject(
                x.Element("Name")?.Value ?? string.Empty,
                x.Element("Abbreviation")?.Value ?? string.Empty,
                x.Element("ParentSubjectName")?.Value ?? string.Empty
            )) ?? Enumerable.Empty<Subject>();

        return subjects;
    }

    public IEnumerable<User> LoadUsers()
    {
        var users = _document.Root?
            .Element("Users")?
            .Elements("User")
            .Select(x => new User
            {
                Login = x.Element("Login")?.Value ?? string.Empty,
                Password = x.Element("Password")?.Value ?? string.Empty
            }) ?? Enumerable.Empty<User>();

        return users;
    }

    public University LoadRandomUniversity()
    {
        var universities = LoadUniversities().ToList();
        if (universities.Count == 0)
            throw new InvalidOperationException("Нет доступных университетов в данных");

        return universities[_random.Next(universities.Count)];
    }

    public Subject LoadRandomSubject()
    {
        var subjects = LoadSubjects().ToList();
        if (subjects.Count == 0)
            throw new InvalidOperationException("Нет доступных предметов в данных");

        return subjects[_random.Next(subjects.Count)];
    }

    public User LoadRandomUser()
    {
        var users = LoadUsers().ToList();
        if (users.Count == 0)
            throw new InvalidOperationException("Нет доступных пользователей в данных");

        return users[_random.Next(users.Count)];
    }

    public University? LoadUniversityByName(string name)
    {
        return LoadUniversities().FirstOrDefault(u => u.Name == name);
    }

    public Subject? LoadSubjectByName(string name)
    {
        return LoadSubjects().FirstOrDefault(s => s.Name == name);
    }

    public User? LoadUserByLogin(string login)
    {
        return LoadUsers().FirstOrDefault(u => u.Login == login);
    }
}
