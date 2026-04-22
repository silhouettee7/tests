namespace StudFileTests.Helpers;

public abstract class EntityHelper<T>(AppManager manager) : HelperBase(manager)
{
    public abstract EntityHelper<T> FillNewEntityFields(T entity);
    public abstract EntityHelper<T> CreateNewEntity();
}