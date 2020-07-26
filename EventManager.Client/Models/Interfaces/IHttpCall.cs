using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Models.Interfaces
{
    public interface IHttpCall<TList, TSimple, TModel>
    {
        Task<List<TList>> GetAll();
        Task<TSimple> Get(int id);
        Task<bool> Create(TModel model);
        Task<bool> Update(int id, TModel model);
        Task<bool> Delete(int id);
    }
}