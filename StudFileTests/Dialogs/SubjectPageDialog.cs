using OpenQA.Selenium;
using StudFileTests.Abstractions;
using StudFileTests.Entities;

namespace StudFileTests.Dialogs;

public class SubjectPageDialog: PageDialog<Subject>
{
    private Subject? _subject;
    public override PageDialog<Subject> OpenPopupFormToCreateNewEntity()
    {
        if (Driver == null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("showPopupAddSubject")).Click();
        Driver.FindElement(By.Id("_tab__add_new_subj")).Click();

        return this;
    }

    public override PageDialog<Subject> FillNewEntityFields()
    {
        if (_subject is null || Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("txtAbbreviationSubj")).Click();
        Driver.FindElement(By.Id("txtAbbreviationSubj")).SendKeys(_subject.Abbreviation);
        Driver.FindElement(By.Id("txtNameSubj")).Click();
        Driver.FindElement(By.Id("txtNameSubj")).SendKeys(_subject.Name);
        Driver.FindElement(By.Id("ddlParent")).Click();
        {
            var dropdown = Driver.FindElement(By.Id("ddlParent"));
            dropdown.FindElement(By.XPath($"//option[. = '{_subject.ParentSubjectName}']")).Click();
        }

        return this;
    }

    public override PageDialog<Subject> CreateNewEntity()
    {
        if (Driver is null) throw new InvalidOperationException();
        
        Driver.FindElement(By.Id("btnAddSubject")).Click();

        return this;
    }

    public override void Setup(IWebDriver driver, Subject entity)
    {
        _subject = entity;
        base.Setup(driver, entity);
    }
}