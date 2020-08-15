using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface INewsService : IRepository<News>
    {
    }
}