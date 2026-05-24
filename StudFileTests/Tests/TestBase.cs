namespace StudFileTests.Tests;

public abstract class TestBase(AppManager appManager) : IClassFixture<AppManager>
{
    protected readonly AppManager AppManager = appManager;
}
