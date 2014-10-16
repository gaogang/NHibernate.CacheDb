using NHibernate.CacheDb.Models;

namespace NHibernate.CacheDb.Repositories
{
    public class ProductRepository : IRepository<GGTestProduct>
    {
        public GGTestProduct Get(GGTestProduct entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<GGTestProduct>(entity.Id);
            }
        }
    }
}
