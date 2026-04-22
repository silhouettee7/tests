namespace StudFileTests.Helpers;

public class WindowHelper(AppManager manager) : HelperBase(manager)
{
    private const string BaseUrl = "https://studfile.net/";
    
    public void Open()
    {
        Driver.Navigate().GoToUrl(BaseUrl);
    }

    public void Close()
    {
        Driver.Quit();
    }
}