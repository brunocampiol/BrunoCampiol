using BrunoCampiol.Infra.Data.Models;
using System.Threading.Tasks;

namespace BrunoCampiol.Domain.Interfaces
{
    public interface IVisitorService
    {
        void HandleVisitor(VISITORS visitor);

        Task HandleVisitorAsync(VISITORS visitor);
    }
}