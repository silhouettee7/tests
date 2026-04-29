using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using StudFileTests.Entities;

namespace StudFileTests.Helpers;

public class LoginHelper(AppManager manager, IConfiguration configuration) : HelperBase(manager)
{
    private const string UserSectionName = "User";
    private bool _isLogined;
    
    public void Login()
    {
        var user = configuration.GetSection(UserSectionName).Get<User>() ?? new User();
        
        if (string.IsNullOrEmpty(user.Login)) throw new Exception("Invalid Login");
        if (string.IsNullOrEmpty(user.Password)) throw new Exception("Invalid Password");
        
        if (_isLogined) return;
        Driver.FindElement(By.LinkText("Войти")).Click();
        Driver.FindElement(By.Id("txtPassword")).SendKeys(user.Password);
        Driver.FindElement(By.Id("txtLogin")).SendKeys(user.Login);
        Driver.FindElement(By.Id("btnLogin")).Click();
        _isLogined = true;
    }
}