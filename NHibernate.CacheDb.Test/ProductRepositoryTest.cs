using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.CacheDb.Models;
using NHibernate.CacheDb.Repositories;

namespace NHibernate.CacheDb.Test
{
    /// <summary>
    /// Summary description for WorklistDataItemRepositoryTest
    /// </summary>
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void Get_GetTestProduct_ShouldReturnExpectedValue()
        {
            var repository = new ProductRepository();
            var entity = new GGTestProduct
            {
                Id = 1
            };

            var actual = repository.Get(entity);

            Assert.IsNotNull(actual);
            Assert.AreEqual("Test Product 1", actual.Name);
            Assert.AreEqual("Test", actual.Category);
        }
    }
}
