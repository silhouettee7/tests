using System.Xml.Linq;
using StudFileTests.Entities;

namespace StudFileTests.TestData;

/// <summary>
/// Загрузчик тестовых данных из XML файла
/// Предоставляет методы для получения данных по различным критериям
/// </summary>
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

    /// <summary>
    /// Загружает все университеты из XML
    /// </summary>
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

    /// <summary>
    /// Загружает все предметы из XML
    /// </summary>
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

    /// <summary>
    /// Загружает всех пользователей из XML
    /// </summary>
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

    /// <summary>
    /// Загружает случайный университет
    /// </summary>
    public University LoadRandomUniversity()
    {
        var universities = LoadUniversities().ToList();
        if (universities.Count == 0)
            throw new InvalidOperationException("Нет доступных университетов в данных");

        return universities[_random.Next(universities.Count)];
    }

    /// <summary>
    /// Загружает случайный предмет
    /// </summary>
    public Subject LoadRandomSubject()
    {
        var subjects = LoadSubjects().ToList();
        if (subjects.Count == 0)
            throw new InvalidOperationException("Нет доступных предметов в данных");

        return subjects[_random.Next(subjects.Count)];
    }

    /// <summary>
    /// Загружает случайного пользователя
    /// </summary>
    public User LoadRandomUser()
    {
        var users = LoadUsers().ToList();
        if (users.Count == 0)
            throw new InvalidOperationException("Нет доступных пользователей в данных");

        return users[_random.Next(users.Count)];
    }

    /// <summary>
    /// Поиск университета по имени
    /// </summary>
    public University? LoadUniversityByName(string name)
    {
        return LoadUniversities().FirstOrDefault(u => u.Name == name);
    }

    /// <summary>
    /// Поиск предмета по имени
    /// </summary>
    public Subject? LoadSubjectByName(string name)
    {
        return LoadSubjects().FirstOrDefault(s => s.Name == name);
    }

    /// <summary>
    /// Поиск пользователя по логину
    /// </summary>
    public User? LoadUserByLogin(string login)
    {
        return LoadUsers().FirstOrDefault(u => u.Login == login);
    }
}
