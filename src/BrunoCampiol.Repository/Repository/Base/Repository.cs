using BrunoCampiol.Infra.Data.Context;
using BrunoCampiol.Infra.Data.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrunoCampiol.Infra.Data.Repository.Base
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private DatabaseContext _dbContext;

        public Repository(DatabaseContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return query;
        }

        public IQueryable<T> GetAllNoTrack()
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();
            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetNoTrack(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking().Where(predicate);
            return query;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}