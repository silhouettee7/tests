using OpenQA.Selenium;
using StudFileTests.Entities;

namespace StudFileTests.Helpers;

public class UniversityHelper(AppManager manager) : EntityHelper<University>(manager)
{
    public override EntityHelper<University> FillNewEntityFields(University entity)
    {
        Driver.FindElement(By.Id("txtAbbreviation")).Click();
        Driver.FindElement(By.Id("txtAbbreviation")).SendKeys(entity.Abbreviation);
        Driver.FindElement(By.CssSelector("#addUniversityForm > label:nth-child(4)")).Click();
        Driver.FindElement(By.Id("txtNameUniver")).SendKeys(entity.Name);
        Driver.FindElement(By.Id("txtCity")).Click();
        Driver.FindElement(By.Id("txtCity")).SendKeys(entity.City);

        return this;
    }

    public override EntityHelper<University> CreateNewEntity()
    {
        Driver.FindElement(By.Id("addUniversityForm")).Click();

        return this;
    }
}