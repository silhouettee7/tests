using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace StudFileTests.Tests;

public abstract class TestBase: IDisposable
{
    protected readonly AppManager AppManager = new AppManager();
    public IWebDriver Driver {get; }
    public IDictionary<String, Object> Vars {get; private set;}
    public IJavaScriptExecutor Js {get; private set;}

    protected TestBase()
    {
        Driver = new FirefoxDriver();
        Js = (IJavaScriptExecutor)Driver;
        Vars = new Dictionary<String, Object>();
    }
    
    public void Dispose()
    {
        AppManager.Dispose();
    }
}