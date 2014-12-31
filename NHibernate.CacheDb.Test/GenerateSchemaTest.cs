using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.CacheDb.Sample.Models;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.CacheDb.Test
{
    [TestClass]
    public class GenerateSchemaTest
    {
        /// <summary>
        /// This will drop and recreate the GGTestProduct table
        /// </summary>
        [Ignore]
        [TestMethod]
        public void Can_generate_schema()
        {
            var config = new Configuration();
            
            config.Configure();
            config.AddAssembly(typeof(GGTestProduct).Assembly);

            new SchemaExport(config).Execute(false, true, false);
        }
    }
}
