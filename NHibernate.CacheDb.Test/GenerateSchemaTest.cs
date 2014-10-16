using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.CacheDb.Models;

namespace NHibernate.CacheDb.Test
{
    [TestClass]
    public class GenerateSchemaTest
    {
        /// <summary>
        /// This will drop and recreate the GGTestProduct table
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Can_generate_schema()
        {
            var config = new Configuration();
            
            config.Configure();
            config.AddAssembly(typeof(GGTestProduct).Assembly);

            new SchemaExport(config).Execute(false, true, false);
        }
    }
}
