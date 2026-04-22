using OpenQA.Selenium;

namespace StudFileTests.Helpers;

public class NavigationHelper(AppManager manager) : HelperBase(manager)
{
    public void MoveToMyFilesSection()
    {
        var element = "Мои файлы";
        var linkText = By.LinkText(element);
        if (!IsElementPresent(linkText))
        {
            throw new Exception();
        }
        Driver.FindElement(linkText).Click();
    }
    
    public void OpenPopupFormToCreateSubject()
    {
        Driver.FindElement(By.Id("showPopupAddSubject")).Click();
        Driver.FindElement(By.Id("_tab__add_new_subj")).Click();
    }
    
    public void OpenPopupFormToCreateNewUniversity()
    {
        Driver.FindElement(By.Id("showPopupAddUniversity")).Click();
        Driver.FindElement(By.Id("tab___add_new")).Click();
    }
}