namespace StudFileTests.Tests;

public class AuthenticationTests(AppManager appManager) : AuthBase(appManager)
{
    [Fact]
    public void LoginWithValidCredentials()
    {
        LogTestStart("LoginWithValidCredentials");
        try
        {
            LogAuthAction("Attempting login with valid credentials");
            AppManager.Auth.Login();
            CheckAuthStatus();
            Assert.True(AppManager.Auth.IsLogined);
            LogTestEnd("LoginWithValidCredentials");
        }
        catch (Exception ex)
        {
            LogError($"Test failed: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public void SmartLoginDoesNotReloginIfAlreadyLogined()
    {
        LogTestStart("SmartLoginDoesNotReloginIfAlreadyLogined");
        try
        {
            LogAuthAction("First login");
            AppManager.Auth.Login();
            Assert.True(AppManager.Auth.IsLogined);
            
            LogAuthAction("Second login attempt (should be skipped)");
            AppManager.Auth.Login();
            Assert.True(AppManager.Auth.IsLogined);
            LogTestEnd("SmartLoginDoesNotReloginIfAlreadyLogined");
        }
        catch (Exception ex)
        {
            LogError($"Test failed: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public void ForceLoginRelogin()
    {
        LogTestStart("ForceLoginRelogin");
        try
        {
            LogAuthAction("First login");
            AppManager.Auth.Login();
            Assert.True(AppManager.Auth.IsLogined);
            
            LogAuthAction("Force login (relogin)");
            AppManager.Auth.ForceLogin();
            Assert.True(AppManager.Auth.IsLogined);
            LogTestEnd("ForceLoginRelogin");
        }
        catch (Exception ex)
        {
            LogError($"Test failed: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public void LogoutClearsAuthStatus()
    {
        LogTestStart("LogoutClearsAuthStatus");
        try
        {
            LogAuthAction("Login");
            AppManager.Auth.Login();
            Assert.True(AppManager.Auth.IsLogined);
            
            LogAuthAction("Logout");
            AppManager.Auth.Logout();
            Assert.False(AppManager.Auth.IsLogined);
            LogTestEnd("LogoutClearsAuthStatus");
        }
        catch (Exception ex)
        {
            LogError($"Test failed: {ex.Message}");
            throw;
        }
    }
}
