using BrunoCampiol.Repository.Context;
using BrunoCampiol.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BrunoCampiol.Repository.Generic
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private DatabaseContext databaseContext;

        //public Repository()
        //{
        //    DbContextOptionsBuilder<ObdEmailContext> optionsBuilder = new DbContextOptionsBuilder<ObdEmailContext>();
        //    optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=BrunoCampiol;Integrated Security=True");

        //    Log.Entry(LogLevel.Warning, "Using default (localhost) repository connection");

        //    databaseContext = new ObdEmailContext(optionsBuilder.Options);
        //}

        public Repository(DatabaseContext context)
        {
            databaseContext = context;
        }

        //public void SetDbContext(DbContextOptionsBuilder<BrunoCampiolContext> optionsBuilder)
        //{
        //    databaseContext = new BrunoCampiolContext(optionsBuilder.Options);
        //}

        //public void SetDbContext(BrunoCampiolContext context)
        //{
        //    databaseContext = context;
        //}

        public IQueryable<T> GetAll()
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            IQueryable<T> query = databaseContext.Set<T>();
            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            IQueryable<T> query = databaseContext.Set<T>().Where(predicate);
            return query;
        }

        public void Add(T entity)
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            databaseContext.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            databaseContext.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            databaseContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            if (databaseContext == null) throw new ArgumentNullException("ObdEmailContext", "Cannot be null");

            databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            databaseContext.Dispose();
        }
    }
}
