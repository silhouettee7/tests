using StudFileTests.Entities;

namespace StudFileTests.Tests;

public class StudFileTests(AppManager appManager) : TestBase(appManager)
{
    [Fact]
    public void AuthOnSite() 
    {
      AppManager.Auth.Login();
    }
  
    [Fact]
    public void CreateNewSubject()
    {
      var subject = new Subject("Конкретная математика","КМ", "Математика");
      
      AppManager.Auth.Login();
      AppManager.Navigation.MoveToMyFilesSection();
      AppManager.Navigation.OpenPopupFormToCreateSubject();
      AppManager.Subject
        .FillNewEntityFields(subject)
        .CreateNewEntity();
      
      var createdSubject = AppManager.Subject.GetCreatedEntityName(subject.Name);
      
      Assert.Equal(subject.Name, createdSubject);
    }
  
    [Fact]
    public void CreateNewUniversity()
    {
      var university = new University("Высшее учебное заведение", "ВУЗ", "Казань");
      AppManager.Auth.Login();
      AppManager.Navigation.MoveToMyFilesSection();
      AppManager.Navigation.OpenPopupFormToCreateNewUniversity();
      AppManager.University
        .FillNewEntityFields(university)
        .CreateNewEntity();
      
      var createdUniversity = AppManager.University.GetCreatedEntityName(university.Name);
      
      Assert.Equal(university.Name, createdUniversity);
    }
}