using NHibernate;
using NHibernate.Cfg;
using NHibernate.CacheDb.Models;

namespace NHibernate
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();

                    configuration.Configure();
                    configuration.AddAssembly(typeof(GGTestProduct).Assembly);
                    
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
