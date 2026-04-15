using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using StudFileTests.Entities;

namespace StudFileTests.Abstractions;

public abstract class TestBase: IDisposable
{
    private const string SecretsPath = "secrets.json";
    private const string SecretsUserPath = "secrets.user.json";
    private const string BaseUrl = "https://studfile.net/";
    private const string UserSectionName = "User";
    public IConfiguration Configuration { get; }
    public IWebDriver Driver {get; }
    public IDictionary<String, Object> Vars {get; private set;}
    public IJavaScriptExecutor Js {get; private set;}

    protected TestBase()
    {
        Driver = new FirefoxDriver();
        Js = (IJavaScriptExecutor)Driver;
        Vars = new Dictionary<String, Object>();
        Configuration = SetConfiguration();
    }
    
    protected void Auth()
    {
        var user = Configuration.GetSection(UserSectionName).Get<User>() ?? new User();
        
        if (string.IsNullOrEmpty(user.Login)) throw new Exception("Invalid Login");
        if (string.IsNullOrEmpty(user.Password)) throw new Exception("Invalid Password");
        
        Driver.FindElement(By.LinkText("Войти")).Click();
        Driver.FindElement(By.Id("txtPassword")).SendKeys(user.Password);
        Driver.FindElement(By.Id("txtLogin")).SendKeys(user.Login);
        Driver.FindElement(By.Id("btnLogin")).Click();
    }

    protected void OpenWindow()
    {
        Driver.Navigate().GoToUrl(BaseUrl);
    }
    
    protected void CloseWindow()
    {
        Driver.Close();
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
    
    public void Dispose()
    {
        Driver.Quit();
    }
}