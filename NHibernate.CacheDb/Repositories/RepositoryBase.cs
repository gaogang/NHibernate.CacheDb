using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.CacheDb.Repositories
{
    public class RepositoryBase<T> : IDisposable where T : class
    {
        private bool _disposed = false;

        private ISession _session;

        public ISession Session
        {
            get
            {
                return _session;
            }
        }

        /// <summary>
        /// Fetch an entity from the database table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected T Get(int id)
        {
            InitialiseSessionIfRequired();

            return _session.Get<T>(id);
        }

        /// <summary>
        /// Query the database table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            InitialiseSessionIfRequired();
                
            return _session.QueryOver<T>()
                    .Where(predicate)
                    .List();
        }

        /// <summary>
        /// Return all the records
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected IEnumerable<T> GetAll()
        {
            InitialiseSessionIfRequired();
            
            return _session.QueryOver<T>().List();
        }

        /// <summary>
        /// Insert a new entity into the database table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected int Create(T entity)
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

        protected void InitialiseSessionIfRequired()
        {
            if (_session == null)
            {
                _session = NHibernateHelper.OpenSession();
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_session != null)
                    {
                        _session.Dispose();
                        _session = null;
                    }

                    _disposed = true;
                }
            }
        }

        #endregion
    }
}
