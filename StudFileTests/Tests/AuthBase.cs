namespace StudFileTests.Tests;

public abstract class AuthBase(AppManager appManager) : TestBase(appManager)
{
    private static readonly object LockObject = new();
    
    protected void LogTestStart(string testName)
    {
        lock (LockObject)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] START: {testName}");
        }
    }

    protected void LogTestEnd(string testName)
    {
        lock (LockObject)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] END: {testName}");
        }
    }

    protected void LogInfo(string message)
    {
        lock (LockObject)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] INFO: {message}");
        }
    }

    protected void LogError(string message)
    {
        lock (LockObject)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ERROR: {message}");
        }
    }

    protected void CheckAuthStatus()
    {
        var isLogined = AppManager.Auth.IsLogined;
        LogInfo($"Auth status: {(isLogined ? "Logged in" : "Not logged in")}");
    }

    protected void LogAuthAction(string action)
    {
        LogInfo($"Auth action: {action}");
    }
}
