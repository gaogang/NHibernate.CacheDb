using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.CacheDb.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Fetch an entity from the database table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        /// <summary>
        /// Query the database table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<T>()
                    .Where(predicate).List();
            }
        }

        /// <summary>
        /// Insert a new entity into the database table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transactionScope = session.BeginTransaction())
                {
                    int id = (int)session.Save(entity);

                    transactionScope.Commit();

                    return id;
                }
            }


        }
    }
}
