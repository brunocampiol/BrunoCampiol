using BrunoCampiol.Infra.Data.Context;
using BrunoCampiol.Infra.Data.Interfaces;
using BrunoCampiol.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrunoCampiol.Infra.Data.Repository
{
    public class VisitorRepository : IVisitorRepository, IDisposable
    {
        private readonly DatabaseContext _db;
        private readonly DbSet<VISITORS> _dbSet;

        public VisitorRepository(DatabaseContext databaseContext)
        {
            _db = databaseContext;
            _dbSet = _db.Set<VISITORS>();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public ICollection<VISITORS> GetPagedVisitors(int page, int pageSize)
        {
            if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            IQueryable<VISITORS> visitorListQuery = _dbSet.AsNoTracking()
                                                          .OrderByDescending(visitor => visitor.CREATED_ON_UTC)
                                                          .Skip((page - 1) * pageSize).Take(pageSize);

            var visitors = visitorListQuery.ToList();
            return visitors;
        }

        public async Task<ICollection<VISITORS>> GetPagedVisitorsAsync(int page, int pageSize)
        {
            if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            IQueryable<VISITORS> visitorListQuery = _dbSet.AsNoTracking()
                                                          .OrderByDescending(visitor => visitor.CREATED_ON_UTC)
                                                          .Skip((page - 1) * pageSize).Take(pageSize);

            var visitors = await visitorListQuery.ToListAsync();
            return visitors;
        }

        public bool Exists(string ipAddress)
        {
            return _dbSet.AsNoTracking().Any(x => x.IP == ipAddress);
        }

        public async Task<bool> ExistsAsync(string ipAddress)
        {
            return await _dbSet.AsNoTracking().AnyAsync(x => x.IP == ipAddress);
        }

        // TODO: use unit of work instead to save changes
        public int Add(VISITORS visitor)
        {
            _dbSet.Add(visitor);
            return _db.SaveChanges();
        }

        // TODO: use unit of work instead to save changes
        public async Task<int> AddAsync(VISITORS visitor)
        {
            await _dbSet.AddAsync(visitor);
            return await _db.SaveChangesAsync();
        }
    }
}