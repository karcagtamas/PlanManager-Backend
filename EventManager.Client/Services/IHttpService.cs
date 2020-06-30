using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public interface IHttpService
    {
        Task<T> get<T>(HttpSettings settings);
        Task<bool> delete(HttpSettings settings);
        Task<bool> update<T>(HttpSettings settings, HttpBody<T> body);
        Task<bool> create<T>(HttpSettings settings, HttpBody<T> body);
    }
}