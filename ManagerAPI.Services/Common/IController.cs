using ManagerAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common
{
    public interface IController<TEntity, TModel> where TEntity : class, IEntity
    {
        IActionResult Get(int id);
        IActionResult GetAll();
        IActionResult Delete(int id);
        IActionResult Update(int id, TModel model);
        IActionResult Create(TModel model);
    }
}
