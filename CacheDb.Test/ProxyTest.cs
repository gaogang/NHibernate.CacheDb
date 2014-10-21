using System;
using InterSystems.Data.CacheClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using User;

namespace CacheDb.Test
{
    [TestClass]
    public class ProxyTest
    {
        [TestMethod]
        public void WorklistDataItem_OpenId_ShouldReturnAnInstanceOfWorklistDataItem()
        {
            var cacheConnect = new CacheConnection();

            cacheConnect.ConnectionString = "Server=Cache3; Namespace = RMH; Password = PASSWORD01; User ID = GAOG;";
            cacheConnect.Open();

            GGTestProduct product = null;

            try
            {
                product = GGTestProduct.OpenId(cacheConnect, "1");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                if (product != null)
                {
                    Assert.AreEqual("Test Product 1", product.Name, "product name does not matched");
                    Assert.AreEqual("Test", product.Category.ToString(), "product category does not matched");

                    product.Close();
                }
                else
                {
                    Assert.Inconclusive("Test data does not exist");
                }

                cacheConnect.Close();
            }
        }
    }
}
