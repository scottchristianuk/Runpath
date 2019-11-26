using System.Threading.Tasks;

namespace Runpath.Media.Net
{
    public interface IApiClient
    {
        Task<T> Get<T>(string path);
    }
}