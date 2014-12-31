using System.Collections.Generic;
using NHibernate.CacheDb.Repositories;
using NHibernate.CacheDb.Sample.Models;

namespace NHibernate.CacheDb.Sample.Repositories
{
    public class ProductRepository : 
        RepositoryBase<GGTestProduct>, 
        IProductRepository
    {
        /// <summary>
        /// Fetch an entity from the database table by Category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<GGTestProduct> GetByCategory(string category)
        {
            return GetBy(p => p.Category == category);
        }
    }
}
