using BrunoCampiol.Repository.Models;

namespace BrunoCampiol.Service.Interface
{
    public interface IIPGeolocationService
    {
        VISITORS GetVisitorInformation(string ipAddress);
    }
}