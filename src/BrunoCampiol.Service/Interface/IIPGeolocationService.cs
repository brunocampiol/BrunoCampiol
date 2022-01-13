using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Domain.Interface
{
    public interface IIPGeolocationService
    {
        VISITORS GetVisitorInformation(string ipAddress);
    }
}