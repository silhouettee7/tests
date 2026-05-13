using StudFileTests.Entities;
using StudFileTests.TestData;

namespace StudFileTests.Tests;

public class StudFileTests(AppManager appManager) : TestBase(appManager)
{
    private static readonly string TestDataPath = Path.Combine(
        AppContext.BaseDirectory, 
        "TestData", 
        "testdata.xml"
    );
    
    private static TestDataLoader? _testDataLoader;
    
    private TestDataLoader GetTestDataLoader()
    {
        return _testDataLoader ??= new TestDataLoader(TestDataPath);
    }

    [Fact]
    public void AuthOnSite() 
    {
        AppManager.Auth.Login();
    }
  
    [Fact]
    public void CreateNewSubject()
    {
        var subject = new Subject("Конкретная математика","КМ", "Математика");
        
        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();
        AppManager.Navigation.OpenPopupFormToCreateSubject();
        AppManager.Subject
            .FillNewEntityFields(subject)
            .CreateNewEntity();
        
        var createdSubject = AppManager.Subject.GetCreatedEntityName(subject.Name);
        
        Assert.Equal(subject.Name, createdSubject);
    }
  
    [Fact]
    public void CreateNewUniversity()
    {
        var university = new University("Высшее учебное заведение", "ВУЗ", "Казань");
        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();
        AppManager.Navigation.OpenPopupFormToCreateNewUniversity();
        AppManager.University
            .FillNewEntityFields(university)
            .CreateNewEntity();
        
        var createdUniversity = AppManager.University.GetCreatedEntityName(university.Name);
        
        Assert.Equal(university.Name, createdUniversity);
    }

    /// <summary>
    /// Тест создания предмета с использованием данных из XML
    /// </summary>
    [Fact]
    public void CreateSubjectFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var subject = testDataLoader.LoadRandomSubject();

        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();
        AppManager.Navigation.OpenPopupFormToCreateSubject();
        AppManager.Subject
            .FillNewEntityFields(subject)
            .CreateNewEntity();

        var createdSubject = AppManager.Subject.GetCreatedEntityName(subject.Name);

        Assert.Equal(subject.Name, createdSubject);
    }

    /// <summary>
    /// Тест создания университета с использованием данных из XML
    /// </summary>
    [Fact]
    public void CreateUniversityFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var university = testDataLoader.LoadRandomUniversity();

        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();
        AppManager.Navigation.OpenPopupFormToCreateNewUniversity();
        AppManager.University
            .FillNewEntityFields(university)
            .CreateNewEntity();

        var createdUniversity = AppManager.University.GetCreatedEntityName(university.Name);

        Assert.Equal(university.Name, createdUniversity);
    }

    /// <summary>
    /// Параметризованный тест - создание нескольких предметов из XML данных
    /// </summary>
    [Fact]
    public void CreateMultipleSubjectsFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var subjects = testDataLoader.LoadSubjects().Take(3); // Берем первые 3 предмета

        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();

        foreach (var subject in subjects)
        {
            AppManager.Navigation.OpenPopupFormToCreateSubject();
            AppManager.Subject
                .FillNewEntityFields(subject)
                .CreateNewEntity();

            var createdSubject = AppManager.Subject.GetCreatedEntityName(subject.Name);
            Assert.Equal(subject.Name, createdSubject);
        }
    }

    /// <summary>
    /// Параметризованный тест - создание нескольких университетов из XML данных
    /// </summary>
    [Fact]
    public void CreateMultipleUniversitiesFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var universities = testDataLoader.LoadUniversities().Take(3); // Берем первые 3 университета

        AppManager.Auth.Login();
        AppManager.Navigation.MoveToMyFilesSection();

        foreach (var university in universities)
        {
            AppManager.Navigation.OpenPopupFormToCreateNewUniversity();
            AppManager.University
                .FillNewEntityFields(university)
                .CreateNewEntity();

            var createdUniversity = AppManager.University.GetCreatedEntityName(university.Name);
            Assert.Equal(university.Name, createdUniversity);
        }
    }

    /// <summary>
    /// Тест загрузки всех данных из XML
    /// </summary>
    [Fact]
    public void ValidateTestDataLoading()
    {
        var testDataLoader = GetTestDataLoader();

        var universities = testDataLoader.LoadUniversities();
        var subjects = testDataLoader.LoadSubjects();
        var users = testDataLoader.LoadUsers();

        Assert.NotEmpty(universities);
        Assert.NotEmpty(subjects);
        Assert.NotEmpty(users);

        // Проверяем, что данные загружены корректно
        Assert.True(universities.All(u => !string.IsNullOrEmpty(u.Name)));
        Assert.True(subjects.All(s => !string.IsNullOrEmpty(s.Name)));
        Assert.True(users.All(u => !string.IsNullOrEmpty(u.Login)));
    }

    /// <summary>
    /// Тест поиска конкретного предмета по имени
    /// </summary>
    [Fact]
    public void FindSubjectByNameFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var subject = testDataLoader.LoadSubjectByName("Конкретная математика");

        Assert.NotNull(subject);
        Assert.Equal("Конкретная математика", subject.Name);
        Assert.Equal("КМ", subject.Abbreviation);
    }

    /// <summary>
    /// Тест поиска конкретного университета по имени
    /// </summary>
    [Fact]
    public void FindUniversityByNameFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var university = testDataLoader.LoadUniversityByName("Московский государственный университет");

        Assert.NotNull(university);
        Assert.Equal("Московский государственный университет", university.Name);
        Assert.Equal("МГУ", university.Abbreviation);
    }

    /// <summary>
    /// Тест поиска конкретного пользователя по логину
    /// </summary>
    [Fact]
    public void FindUserByLoginFromTestData()
    {
        var testDataLoader = GetTestDataLoader();
        var user = testDataLoader.LoadUserByLogin("testuser1");

        Assert.NotNull(user);
        Assert.Equal("testuser1", user.Login);
        Assert.Equal("TestPassword123!", user.Password);
    }
}
