
namespace NHibernate.CacheDb.Repositories
{
    public interface IRepository<T>
    {
        T Get(T id);
    }
}
