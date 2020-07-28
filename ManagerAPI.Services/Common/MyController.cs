using System;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Services.Common
{
    public class MyController<TEntity, TModel, TList, TSimple> : ControllerBase, IController<TEntity, TModel> where TEntity : class, IEntity
    {
        protected const string FatalError = "Something bad happened. Try again later";
        protected readonly ILoggerService Logger;
        private readonly IRepository<TEntity> _service;

        public MyController(ILoggerService logger, IRepository<TEntity> service) 
        {
            this.Logger = logger;
            this._service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            try
            {
                this._service.Add<TModel>(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._service.Remove(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(this._service.Get<TSimple>(id));
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(this._service.GetAll<TList>());
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TModel model)
        {
            try
            {
                this._service.Update<TModel>(id, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }
    }
}
