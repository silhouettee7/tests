using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using StudFileTests.Helpers;

namespace StudFileTests;

public class AppManager: IDisposable
{
    private const string SecretsPath = "secrets.json";
    private const string SecretsUserPath = "secrets.user.json";

    private readonly IConfiguration _configuration;
    
    public IWebDriver Driver { get; }

    public NavigationHelper Navigation { get; }

    public LoginHelper Auth { get; }

    public SubjectHelper Subject { get; }

    public UniversityHelper University { get; }
    public WindowHelper Window { get;  }

    public AppManager()
    {
        _configuration = SetConfiguration();
        Driver = new FirefoxDriver();
        
        Auth = new LoginHelper(this, _configuration);
        Navigation = new NavigationHelper(this);
        Subject = new SubjectHelper(this);
        University = new UniversityHelper(this);
        Window = new WindowHelper(this);
        
        Window.Open();
    }
    
    public void Dispose()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    private IConfiguration SetConfiguration()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var configFileNamePath = Path.Combine(currentDirectory, SecretsPath);
        var personalConfigFileNamePath = Path.Combine(currentDirectory, SecretsUserPath);
        var configFileExists = File.Exists(configFileNamePath);
        var personalConfigFileExists = File.Exists(personalConfigFileNamePath);
        if (!configFileExists && !personalConfigFileExists)
        {
            throw new FileNotFoundException("Could not find configuration file");
        }
        var currentConfigFile = personalConfigFileExists 
            ? SecretsUserPath 
            : SecretsPath;
        
        return new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile(currentConfigFile, optional: false, reloadOnChange: true)
            .Build();
    }
}