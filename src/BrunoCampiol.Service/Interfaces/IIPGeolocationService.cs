using BrunoCampiol.Infra.Data.Models;
using System.Threading.Tasks;

namespace BrunoCampiol.Domain.Interfaces
{
    public interface IIPGeolocationService
    {
        VISITORS GetVisitorInformation(string ipAddress);

        Task<VISITORS> GetVisitorInformationAsync(string ipAddress);
    }
}