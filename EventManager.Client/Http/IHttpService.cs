using System.Threading.Tasks;

namespace EventManager.Client.Http
{
    public interface IHttpService
    {
        Task<T> Get<T>(HttpSettings settings);
        Task<string> GetString(HttpSettings settings);
        Task<int?> GetInt(HttpSettings settings);
        Task<bool> Delete(HttpSettings settings);
        Task<bool> Update<T>(HttpSettings settings, HttpBody<T> body);
        Task<bool> Create<T>(HttpSettings settings, HttpBody<T> body);
        Task<string> CreateString<T>(HttpSettings settings, HttpBody<T> body);
    }
}