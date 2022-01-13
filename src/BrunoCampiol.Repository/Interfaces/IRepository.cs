using System;
using System.Linq;
using System.Linq.Expressions;

namespace BrunoCampiol.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllNoTrack();

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetNoTrack(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);

        void Edit(T entity);

        void Save();

        void Dispose();
    }
}