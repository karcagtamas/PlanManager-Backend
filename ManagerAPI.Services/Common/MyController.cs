using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common
{
    public class MyController<TEntity, TModel, TList, TSimple> : ControllerBase, IController<TEntity, TModel>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _service;

        public MyController(IRepository<TEntity> service)
        {
            this._service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            this._service.Add<TModel>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this._service.Remove(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this._service.Get<TSimple>(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(this._service.GetAll<TList>());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TModel model)
        {
            this._service.Update<TModel>(id, model);
            return Ok();
        }
    }
}