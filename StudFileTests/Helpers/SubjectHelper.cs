using OpenQA.Selenium;
using StudFileTests.Entities;

namespace StudFileTests.Helpers;

public class SubjectHelper(AppManager manager) : EntityHelper<Subject>(manager)
{
    public override EntityHelper<Subject> FillNewEntityFields(Subject entity)
    {
        Driver.FindElement(By.Id("txtAbbreviationSubj")).Click();
        Driver.FindElement(By.Id("txtAbbreviationSubj")).SendKeys(entity.Abbreviation);
        Driver.FindElement(By.Id("txtNameSubj")).Click();
        Driver.FindElement(By.Id("txtNameSubj")).SendKeys(entity.Name);
        Driver.FindElement(By.Id("ddlParent")).Click();
        {
            var dropdown = Driver.FindElement(By.Id("ddlParent"));
            dropdown.FindElement(By.XPath($"//option[. = '{entity.ParentSubjectName}']")).Click();
        }

        return this;
    }

    public override EntityHelper<Subject> CreateNewEntity()
    {
        Driver.FindElement(By.Id("btnAddSubject")).Click();

        return this;
    }

    public override string GetCreatedEntityName(string entityName)
    {
        var element = Driver.FindElement(By.XPath($"//span[text()='{entityName}']"));
        return element.Text;
    }
}