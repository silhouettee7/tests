using StudFileTests.Entities;

namespace StudFileTests.Tests;

public class StudFileTests : TestBase
{
    [Fact]
    public void AuthOnSite() 
    {
      AppManager.Window.Open();
      AppManager.Auth.Login();
      AppManager.Window.Close();
    }
  
    [Fact]
    public void CreateNewSubject()
    {
      var subject = new Subject("Конкретная математика","КМ", "Математика");
      
      AppManager.Window.Open();
      AppManager.Auth.Login();
      AppManager.Navigation.MoveToMyFilesSection();
      AppManager.Navigation.OpenPopupFormToCreateSubject();
      AppManager.Subject
        .FillNewEntityFields(subject)
        .CreateNewEntity();
      AppManager.Window.Close();
    }
  
    [Fact]
    public void CreateNewUniversity()
    {
      var university = new University("Высшее учебное заведение", "ВУЗ", "Казань");
      AppManager.Window.Open();
      AppManager.Auth.Login();
      AppManager.Navigation.MoveToMyFilesSection();
      AppManager.Navigation.OpenPopupFormToCreateNewUniversity();
      AppManager.University
        .FillNewEntityFields(university)
        .CreateNewEntity();
      AppManager.Window.Close();
    }
}