using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.CacheDb.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);

        IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        int Create(T entity);
    }
}
