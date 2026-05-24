using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using StudFileTests.Entities;

namespace StudFileTests.Helpers;

public class AuthHelper(AppManager manager, IConfiguration configuration) : HelperBase(manager)
{
    private const string UserSectionName = "User";
    private bool _isLogined;
    
    public void Login()
    {
        if (_isLogined) return;
        PerformLogin();
    }

    public void ForceLogin()
    {
        _isLogined = false;
        PerformLogin();
    }

    public void Logout()
    {
        _isLogined = false;
    }

    public bool IsLogined => _isLogined;

    private void PerformLogin()
    {
        var user = configuration.GetSection(UserSectionName).Get<User>() ?? new User();
        
        if (string.IsNullOrEmpty(user.Login)) throw new Exception("Invalid Login");
        if (string.IsNullOrEmpty(user.Password)) throw new Exception("Invalid Password");
        
        Driver.FindElement(By.LinkText("Войти")).Click();
        Driver.FindElement(By.Id("txtLogin")).SendKeys(user.Login);
        Driver.FindElement(By.Id("txtPassword")).SendKeys(user.Password);
        Driver.FindElement(By.Id("btnLogin")).Click();
        _isLogined = true;
    }
}
