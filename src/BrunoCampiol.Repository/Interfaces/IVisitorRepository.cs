using BrunoCampiol.Infra.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrunoCampiol.Infra.Data.Interfaces
{
    public interface IVisitorRepository
    {
        int Add(VISITORS visitor);
        Task<int> AddAsync(VISITORS visitor);
        bool Exists(string ipAddress);
        Task<bool> ExistsAsync(string ipAddress);
        ICollection<VISITORS> GetPagedVisitors(int page, int pageSize);
        Task<ICollection<VISITORS>> GetPagedVisitorsAsync(int page, int pageSize);
        void Dispose();
    }
}