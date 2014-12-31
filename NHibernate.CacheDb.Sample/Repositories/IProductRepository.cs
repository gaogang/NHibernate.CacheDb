using System.Collections.Generic;
using NHibernate.CacheDb.Sample.Models;

namespace NHibernate.CacheDb.Sample.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<GGTestProduct> GetByCategory(string category);
    }
}
