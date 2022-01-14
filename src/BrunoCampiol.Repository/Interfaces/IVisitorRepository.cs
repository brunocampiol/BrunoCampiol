using BrunoCampiol.Infra.Data.Models;
using System.Threading.Tasks;

namespace BrunoCampiol.Infra.Data.Interfaces
{
    public interface IVisitorRepository
    {
        int Add(VISITORS visitor);
        Task<int> AddAsync(VISITORS visitor);
        bool Exists(string ipAddress);
        Task<bool> ExistsAsync(string ipAddress);
        void Dispose();
    }
}