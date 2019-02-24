using System.Threading.Tasks;

namespace BrunoCampiol.Service.Interface
{
    public interface IWebClient
    {
        Task<string> DownloadString();
    }
}
