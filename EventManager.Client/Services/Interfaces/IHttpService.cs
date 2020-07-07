using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services.Interfaces {
    public interface IHttpService {
        Task<T> get<T> (HttpSettings settings);
        Task<string> getString (HttpSettings settings);
        Task<int?> getInt(HttpSettings settings);
        Task<bool> delete (HttpSettings settings);
        Task<bool> update<T> (HttpSettings settings, HttpBody<T> body);
        Task<bool> create<T> (HttpSettings settings, HttpBody<T> body);
        Task<string> createString<T> (HttpSettings settings, HttpBody<T> body);
    }
}