using System.Xml.Linq;
using StudFileTests.Entities;

namespace StudFileTests.TestData;

/// <summary>
/// Генератор тестовых данных для xUnit тестов
/// Создает XML файл со структурированными данными для университетов, предметов и пользователей
/// </summary>
public class TestDataGenerator
{
    private readonly string _outputPath;

    public TestDataGenerator(string outputPath = "TestData/testdata.xml")
    {
        _outputPath = outputPath;
    }

    /// <summary>
    /// Генерирует и сохраняет тестовые данные в XML файл
    /// </summary>
    public void GenerateAndSaveTestData()
    {
        var testData = new XDocument(
            new XElement("TestData",
                new XElement("Universities", GenerateUniversities()),
                new XElement("Subjects", GenerateSubjects()),
                new XElement("Users", GenerateUsers())
            )
        );

        // Убедимся, что директория существует
        var directory = Path.GetDirectoryName(_outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory ?? throw new InvalidOperationException());
        }

        testData.Save(_outputPath);
    }

    /// <summary>
    /// Генерирует список университетов
    /// </summary>
    private IEnumerable<XElement> GenerateUniversities()
    {
        var universitiesData = new[]
        {
            ("Московский государственный университет", "МГУ", "Москва"),
            ("Санкт-Петербургский государственный университет", "СПбГУ", "Санкт-Петербург"),
            ("Новосибирский государственный университет", "НГУ", "Новосибирск"),
            ("Казанский федеральный университет", "КФУ", "Казань"),
            ("Уральский федеральный университет", "УрФУ", "Екатеринбург"),
            ("Томский государственный университет", "ТГУ", "Томск"),
            ("Высшее учебное заведение", "ВУЗ", "Казань"),
            ("Федеральный исследовательский центр", "ФИЦ", "Новосибирск")
        };

        return universitiesData.Select(u => new XElement("University",
            new XElement("Name", u.Item1),
            new XElement("Abbreviation", u.Item2),
            new XElement("City", u.Item3)
        ));
    }

    /// <summary>
    /// Генерирует список предметов
    /// </summary>
    private IEnumerable<XElement> GenerateSubjects()
    {
        var subjectsData = new[]
        {
            // Математика
            ("Конкретная математика", "КМ", "Математика"),
            ("Линейная алгебра", "ЛА", "Математика"),
            ("Математический анализ", "МА", "Математика"),
            ("Дифференциальные уравнения", "ДУ", "Математика"),
            ("Теория вероятностей", "ТВ", "Математика"),
            ("Математическая статистика", "МС", "Математика"),
            
            // Информатика
            ("Программирование", "ПР", "Информатика"),
            ("Структуры данных", "СД", "Информатика"),
            ("Алгоритмы", "АЛ", "Информатика"),
            ("Базы данных", "БД", "Информатика"),
            ("Веб-разработка", "ВР", "Информатика"),
            ("Машинное обучение", "МО", "Информатика"),
            
            // Физика
            ("Классическая механика", "КМ", "Физика"),
            ("Электромагнетизм", "ЭМ", "Физика"),
            ("Квантовая механика", "КВМ", "Физика"),
            ("Термодинамика", "ТД", "Физика"),
            ("Оптика", "ОП", "Физика"),
            ("Ядерная физика", "ЯФ", "Физика")
        };

        return subjectsData.Select(s => new XElement("Subject",
            new XElement("Name", s.Item1),
            new XElement("Abbreviation", s.Item2),
            new XElement("ParentSubjectName", s.Item3)
        ));
    }

    /// <summary>
    /// Генерирует список тестовых пользователей
    /// </summary>
    private IEnumerable<XElement> GenerateUsers()
    {
        var usersData = new[]
        {
            ("testuser1", "TestPassword123!"),
            ("testuser2", "SecurePass456@"),
            ("testuser3", "MyPassword789#"),
            ("testuser4", "QuickTest101$"),
            ("testuser5", "AdminPass202%"),
            ("testuser6", "DevTest303&"),
            ("testuser7", "QAPassword404*"),
            ("testuser8", "UserTest505(")
        };

        return usersData.Select(u => new XElement("User",
            new XElement("Login", u.Item1),
            new XElement("Password", u.Item2)
        ));
    }
}
