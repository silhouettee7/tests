
namespace StudFileTests.Tests;

//IClassFixture создает между тестами единственный экземпляр AppManager, корректно его открывает и диспоузит
public abstract class TestBase(AppManager appManager) : IClassFixture<AppManager>
{
    protected readonly AppManager AppManager = appManager;
}