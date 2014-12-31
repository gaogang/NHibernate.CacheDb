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
        /// Fetch an entity from the database table by its unique Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public GGTestProduct GetProductById(int id)
        {
            return Get(id);
        }

        /// <summary>
        /// Fetch entities from the database table by Category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<GGTestProduct> GetProductsByCategory(string category)
        {
            return GetBy(p => p.Category == category);
        }

        public int SaveProduct(GGTestProduct product)
        {
            return Create(product);
        }

        public void UpdateProduct(GGTestProduct product)
        {
            Update(product);
        }

        public void DeleteProduct(GGTestProduct product)
        {
            Delete(product);
        }
    }
}
