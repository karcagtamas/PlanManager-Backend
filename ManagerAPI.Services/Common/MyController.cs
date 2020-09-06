using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common
{
    /// <summary>
    /// My Controller
    /// </summary>
    /// <typeparam name="TEntity">Type of Entity</typeparam>
    /// <typeparam name="TModel">Type of Model object</typeparam>
    /// <typeparam name="TList">Type of List object</typeparam>
    /// <typeparam name="TSimple">Type of Simple data object</typeparam>
    public class MyController<TEntity, TModel, TList, TSimple> : ControllerBase, IController<TEntity, TModel>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _service;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="service">Repository service</param>
        public MyController(IRepository<TEntity> service)
        {
            this._service = service;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model">Object model</param>
        /// <returns>Ok state</returns>
        [HttpPost]
        public virtual IActionResult Create([FromBody] TModel model)
        {
            this._service.Add<TModel>(model);
            return Ok();
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>Ok state</returns>
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            this._service.Remove(id);
            return Ok();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>Element in ok state</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this._service.Get<TSimple>(id));
        }

        /// <summary>
        /// Get all element
        /// </summary>
        /// <param name="orderBy">Order by</param>
        /// <param name="direction">Order direction</param>
        /// <returns>List of elements in ok state</returns>
        [HttpGet]
        public IActionResult GetAll([FromQuery] string orderBy, [FromQuery] string direction)
        {
            if (string.IsNullOrEmpty(orderBy) || string.IsNullOrEmpty(direction))
            {
                return Ok(this._service.GetAll<TList>());
            }

            return Ok(this._service.GetOrderedAll<TList>(orderBy, direction));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <param name="model">Model of object</param>
        /// <returns>Ok state</returns>
        [HttpPut("{id}")]
        public virtual IActionResult Update(int id, TModel model)
        {
            this._service.Update<TModel>(id, model);
            return Ok();
        }
    }
}