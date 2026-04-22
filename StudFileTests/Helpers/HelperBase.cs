using OpenQA.Selenium;

namespace StudFileTests.Helpers;

public abstract class HelperBase(AppManager manager)
{
    protected readonly IWebDriver Driver = manager.Driver;

    protected bool IsElementPresent(By by)
    {
        try
        {
            Driver.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}