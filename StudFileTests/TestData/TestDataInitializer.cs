namespace StudFileTests.TestData;

/// <summary>
/// Инициализатор тестовых данных
/// Обеспечивает централизованное управление генерацией и проверкой тестовых данных
/// </summary>
public static class TestDataInitializer
{
    private const string DefaultTestDataPath = "TestData/testdata.xml";

    /// <summary>
    /// Инициализирует (генерирует) тестовые данные если их нет
    /// </summary>
    public static void Initialize(string? customPath = null)
    {
        var testDataPath = customPath ?? DefaultTestDataPath;

        if (!TestDataFileExists(testDataPath))
        {
            var generator = new TestDataGenerator(testDataPath);
            generator.GenerateAndSaveTestData();
        }
    }

    /// <summary>
    /// Проверяет наличие файла тестовых данных
    /// </summary>
    public static bool TestDataFileExists(string? path = null)
    {
        var testDataPath = path ?? DefaultTestDataPath;
        return File.Exists(testDataPath);
    }

    /// <summary>
    /// Получает полный путь к файлу тестовых данных
    /// </summary>
    public static string GetTestDataPath(string? customPath = null)
    {
        var testDataPath = customPath ?? DefaultTestDataPath;
        return Path.Combine(AppContext.BaseDirectory, testDataPath);
    }

    /// <summary>
    /// Пересоздает тестовые данные (удаляет старые и генерирует новые)
    /// </summary>
    public static void RecreateTestData(string? customPath = null)
    {
        var testDataPath = customPath ?? DefaultTestDataPath;
        
        if (File.Exists(testDataPath))
        {
            File.Delete(testDataPath);
        }

        Initialize(testDataPath);
    }
}
