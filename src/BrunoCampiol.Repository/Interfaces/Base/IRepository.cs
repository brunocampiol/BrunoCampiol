﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrunoCampiol.Infra.Data.Interfaces.Base
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

        Task SaveAsync();

        void Dispose();
    }
}