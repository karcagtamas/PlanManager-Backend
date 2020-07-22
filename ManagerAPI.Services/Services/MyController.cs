using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class MyController<TEntity, TModel, TList, TSimple> : ControllerBase, IController<TEntity, TModel> where TEntity : class, IEntity
    {
        protected const string FATAL_ERROR = "Something bad happened. Try again later";
        protected readonly ILoggerService Logger;
        protected readonly IRepository<TEntity> Service;

        public MyController(ILoggerService logger, IRepository<TEntity> service) 
        {
            this.Logger = logger;
            this.Service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            try
            {
                this.Service.Add<TModel>(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.Service.Remove(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(this.Service.Get<TSimple>(id));
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(this.Service.GetAll<TList>());
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TModel model)
        {
            try
            {
                this.Service.Update<TModel>(id, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}
