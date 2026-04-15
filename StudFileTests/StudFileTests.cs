using StudFileTests.Abstractions;
using StudFileTests.Dialogs;
using StudFileTests.Entities;

namespace StudFileTests;

public class StudFileTests : TestBase
{
  private readonly PageDialog<Subject> _subjectDialog = new SubjectPageDialog();
  private readonly PageDialog<University> _universityDialog = new UniversityPageDialog();
  
  [Fact]
  public void AuthOnSite() 
  {
    OpenWindow();
    Auth();
    CloseWindow();
  }
  
  [Fact]
  public void CreateNewSubject()
  {
    var subject = new Subject("Конкретная математика","КМ", "Математика");
    _subjectDialog.Setup(Driver, subject);
    
    OpenWindow();
    Auth();
    
    _subjectDialog
      .MoveToMyFilesSection()
      .OpenPopupFormToCreateNewEntity()
      .FillNewEntityFields()
      .CreateNewEntity();
    
    CloseWindow();
  }
  
  [Fact]
  public void CreateNewUniversity()
  {
    var university = new University("Высшее учебное заведение", "ВУЗ", "Казань");
    _universityDialog.Setup(Driver, university);
    
    OpenWindow();
    Auth();
    
    _universityDialog
      .MoveToMyFilesSection()
      .OpenPopupFormToCreateNewEntity()
      .FillNewEntityFields()
      .CreateNewEntity();
    
    CloseWindow();
  }
}