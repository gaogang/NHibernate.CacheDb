using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.CacheDb.Models;

namespace NHibernate.CacheDb.Repositories
{
    public class ProductRepository : RepositoryBase<GGTestProduct>
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
