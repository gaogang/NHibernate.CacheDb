using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.CacheDb.Sample.Models;
using NHibernate.CacheDb.Sample.Repositories;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.CacheDb.Test
{
    /// <summary>
    /// Summary description for WorklistDataItemRepositoryTest
    /// </summary>
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestInitialize]
        public void Initialise()
        {
            NHibernateHelper.RegisterAssembly(typeof(ProductRepository).Assembly);

            ExportSchema();

            InitialiseData();
        }

        [TestMethod]
        public void GetProductById_GetTestProduct_ShouldReturnExpectedValue()
        {
            using (var repository = new ProductRepository())
            {
                var actual = repository.GetProductById(2);

                Assert.IsNotNull(actual);
                Assert.AreEqual("Test Product 2", actual.Name);
                Assert.AreEqual("Test Category", actual.Category);
            }
        }

        [TestMethod]
        public void GetProductById_ProductDoesNotExist_ShouldReturnNull()
        {
            using (var repository = new ProductRepository())
            {
                var actual = repository.GetProductById(1001);

                Assert.IsNull(actual);
            }
        }

        [TestMethod]
        public void SaveProduct_CreateNewProduct_NewProductShouldBeInsertedToDatabase()
        {
            int id = -1;

            var expected = new GGTestProduct
            {
                Name = "Test Product",
                Category = "Test Category",
                IsExpired = true
            };

            using (var repository = new ProductRepository())
            {
                id = repository.SaveProduct(expected);
            }

            // Verify result
            GGTestProduct actual;

            using (var session = NHibernateHelper.OpenSession())
            {
                actual = session.Get<GGTestProduct>(id);
            }

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.IsExpired, actual.IsExpired);
        }

        [TestMethod]
        [ExpectedException(typeof(StaleObjectStateException))]
        public void UpdateProduct_ProductDoesNotExists_ExceptionThrown()
        {
            int id = 1001;

            var expected = new GGTestProduct
            {
                Id = id,
                Name = "Test Product 1001",
                Category = "Test Category 1001",
                IsExpired = true
            };

            using (var repository = new ProductRepository())
            {
                repository.UpdateProduct(expected);
            }
        }

        [TestMethod]
        public void UpdateProduct_ProductExist_DatabaseWillBeUpdated()
        {
            int id = 1;

            var expected = new GGTestProduct
            {
                Id = id,
                Name = "Test Product 11111",
                Category = "Test Category 11111",
                IsExpired = false
            };

            using (var repository = new ProductRepository())
            {
                repository.UpdateProduct(expected);
            }

            // Verify result
            GGTestProduct actual;

            using (var session = NHibernateHelper.OpenSession())
            {
                actual = session.Get<GGTestProduct>(id);
            }

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.IsExpired, actual.IsExpired);
        }

        [TestMethod]
        public void DeleteProduct_ProductExist_ProductWillbeDeletedFromDatabase()
        {
            int id = 1;

            var expected = new GGTestProduct
            {
                Id = id
            };

            using (var repository = new ProductRepository())
            {
                repository.DeleteProduct(expected);
            }

            // Verify result
            GGTestProduct actual;

            using (var session = NHibernateHelper.OpenSession())
            {
                actual = session.Get<GGTestProduct>(id);
            }

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetProductsByCategory_GetProductByCategory_ShouldReturnAllProductsInExpectedCateogry()
        {
            using (var repository = new ProductRepository())
            {

                var products = repository.GetProductsByCategory("Test Category");

                // Verify result

                Assert.AreEqual(2, products.Count());
                Assert.AreEqual("Test Product 1", products.First().Name);
                Assert.AreEqual("Test Product 2", products.Skip(1).First().Name);
            }
        }

        private void InitialiseData()
        {
            using (var repository = new ProductRepository())
            {
                repository.SaveProduct(
                    new GGTestProduct
                    {
                        Name = "Test Product 1",
                        Category = "Test Category",
                        IsExpired = true
                    });

                repository.SaveProduct(
                    new GGTestProduct
                    {
                        Name = "Test Product 2",
                        Category = "Test Category",
                        IsExpired = false
                    });

                repository.SaveProduct(
                    new GGTestProduct
                    {
                        Name = "Test Product 3",
                        Category = "Test Category 3",
                        IsExpired = true
                    });
            }
        }

        private static void ExportSchema()
        {
            var config = new Configuration();

            config.Configure();
            config.AddAssembly(typeof(GGTestProduct).Assembly);

            new SchemaExport(config).Execute(false, true, false);
        }
    }
}
