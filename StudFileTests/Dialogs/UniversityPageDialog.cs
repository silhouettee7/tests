using OpenQA.Selenium;
using StudFileTests.Abstractions;
using StudFileTests.Entities;

namespace StudFileTests.Dialogs;

public class UniversityPageDialog: PageDialog<University>
{
    private University? _university;
    public override PageDialog<University> OpenPopupFormToCreateNewEntity()
    {
        if (Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("showPopupAddUniversity")).Click();
        Driver.FindElement(By.Id("tab___add_new")).Click();

        return this;
    }

    public override PageDialog<University> FillNewEntityFields()
    {
        if (_university is null || Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("txtAbbreviation")).Click();
        Driver.FindElement(By.Id("txtAbbreviation")).SendKeys(_university.Abbreviation);
        Driver.FindElement(By.CssSelector("#addUniversityForm > label:nth-child(4)")).Click();
        Driver.FindElement(By.Id("txtNameUniver")).SendKeys(_university.Name);
        Driver.FindElement(By.Id("txtCity")).Click();
        Driver.FindElement(By.Id("txtCity")).SendKeys(_university.City);

        return this;
    }

    public override PageDialog<University> CreateNewEntity()
    {
        if (Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("addUniversityForm")).Click();

        return this;
    }

    public override void Setup(IWebDriver driver, University entity)
    {
        _university = entity;
        base.Setup(driver, entity);
    }
}