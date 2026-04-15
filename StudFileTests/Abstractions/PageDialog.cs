using OpenQA.Selenium;

namespace StudFileTests.Abstractions;

public abstract class PageDialog<T>
{
    protected IWebDriver? Driver;

    public abstract PageDialog<T> OpenPopupFormToCreateNewEntity();
    public abstract PageDialog<T> FillNewEntityFields();
    public abstract PageDialog<T> CreateNewEntity();

    public virtual void Setup(IWebDriver driver, T entity)
    {
        Driver = driver;
    }
    
    public PageDialog<T> MoveToMyFilesSection()
    {
        if (Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.LinkText("Мои файлы")).Click();
        
        return this;
    }
}