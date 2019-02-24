using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BrunoCampiol.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);

        void Edit(T entity);

        void Save();

        void Dispose();
    }
}
