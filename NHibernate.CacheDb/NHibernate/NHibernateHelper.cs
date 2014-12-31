using System;
using System.Reflection;
using NHibernate.Cfg;

namespace NHibernate.CacheDb
{
    public class NHibernateHelper
    {
        private static ISessionFactory Session;

        private static Configuration Config;

        public static void RegisterAssembly(Assembly assembly)
        {
            if (Config == null)
            {
                Config = new Configuration();

                Config.Configure();
            }

            Config.AddAssembly(assembly);
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (Config == null)
                {
                    throw new InvalidOperationException("NHibernate configuration is not initialised.");
                }

                if (Session == null)
                {
                    Session = Config.BuildSessionFactory();
                }

                return Session;
            }
        }

        public static ISession OpenSession()
        {
            if (SessionFactory == null)
            {
                throw new InvalidOperationException("NHibernate SessionFactory is not initialised.");
            }

            return SessionFactory.OpenSession();
        }
    }
}
