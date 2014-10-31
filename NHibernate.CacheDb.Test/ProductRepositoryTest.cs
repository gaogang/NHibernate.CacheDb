using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.CacheDb.Models;
using NHibernate.CacheDb.Repositories;
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
            ExportSchema();

            InitialiseData();
        }

        [TestMethod]
        public void Get_GetTestProduct_ShouldReturnExpectedValue()
        {
            var repository = new ProductRepository();

            var actual = repository.Get(2);

            Assert.IsNotNull(actual);
            Assert.AreEqual("Test Product 2", actual.Name);
            Assert.AreEqual("Test Category", actual.Category);
        }

        [TestMethod]
        public void Create_InsertNewProduct_NewProductShouldBeInsertedToDatabase()
        { 
            var repository = new ProductRepository();
            
            var expected = new GGTestProduct
            {
                Name = "Test Product",
                Category = "Test Category",
                IsExpired = true
            };

            int id = repository.Create(expected);

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
        public void GetByCategory_GetProductByCategory_ShouldReturnAllProductsInExpectedCateogry()
        {
            var repository = new ProductRepository();

            var products = repository.GetByCategory("Test Category");

            // Verify result
            
            Assert.AreEqual(2, products.Count());
            Assert.AreEqual("Test Product 1", products.First().Name);
            Assert.AreEqual("Test Product 2", products.Skip(1).First().Name);
        }

        private void InitialiseData()
        {
            var repository = new ProductRepository();

            repository.Create(
                new GGTestProduct
                {
                    Name = "Test Product 1",
                    Category = "Test Category",
                    IsExpired = true
                });

            repository.Create(
                new GGTestProduct
                {
                    Name = "Test Product 2",
                    Category = "Test Category",
                    IsExpired = false
                });

            repository.Create(
                new GGTestProduct
                {
                    Name = "Test Product 3",
                    Category = "Test Category 3",
                    IsExpired = true
                });
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
