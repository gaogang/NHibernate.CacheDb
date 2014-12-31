using System.Collections.Generic;
using NHibernate.CacheDb.Sample.Models;

namespace NHibernate.CacheDb.Sample.Repositories
{
    public interface IProductRepository
    {
        GGTestProduct GetProductById(int id);

        IEnumerable<GGTestProduct> GetProductsByCategory(string category);

        int SaveProduct(GGTestProduct product);

        void UpdateProduct(GGTestProduct product);
    }
}
