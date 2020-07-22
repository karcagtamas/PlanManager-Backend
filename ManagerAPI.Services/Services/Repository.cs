using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext Context;
        protected readonly ILoggerService Logger;
        protected readonly IUtilsService Utils;
        protected readonly IMapper Mapper;
        public Repository(DbContext context, ILoggerService logger, IUtilsService utils, IMapper mapper)
        {
            this.Context = context;
            this.Logger = logger;
            this.Utils = utils;
            this.Mapper = mapper;
        }

        public void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), entity.Id);
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"), entity.Id);
            }
        }

        public void Add<T>(T entity)
        {
            this.Add(this.Mapper.Map<TEntity>(entity));
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().AddRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), entities.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"), entities.Select(x => x.Id).ToList());
            }
        }

        public void AddRange<T>(IEnumerable<T> entities)
        {
            this.AddRange(this.Mapper.Map<IEnumerable<TEntity>>(entities));
        }

        public void Complete()
        {
            this.Context.SaveChanges();
        }

        public TEntity Get(params object[] keys)
        {
            return this.Context.Set<TEntity>().Find(keys);
        }
        
        public T Get<T>(params object[] keys)
        {
            return this.Mapper.Map<T>(this.Get(keys));
        }

        public List<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }

        public List<T> GetAll<T>()
        {
            return this.Mapper.Map<List<T>>(this.GetAll());
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetList(predicate, null, null);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count)
        {
            return this.GetList(predicate, count, null);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count, int? skip)
        {
            var query = this.Context.Set<TEntity>().Where(predicate);

            if (count != null)
            {
                query = query.Take((int)count);
            }

            if (skip != null)
            {
                query = query.Skip((int)skip);
            }

            return query.ToList();
        }

        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate));
        }

        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate, int? count)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate, count));

        }

        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate, int? count, int? skip)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate, count, skip));
        }

        public void Remove(TEntity entity)
        {
            this.Context.Set<TEntity>().Remove(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"), entity.Id);
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("delete"), entity.Id);
            }
        }

        public void Remove(int id)
        {
            var entity = this.Get(id);

            if (entity == null)
            {
                var user = this.Utils.GetCurrentUser();
                throw this.Logger.LogInvalidThings(user, this.GetService(), "id", this.GetEntityErrorMessage());
            }

            this.Remove(this.Get(id));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().RemoveRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"), entities.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("delete"), entities.Select(x => x.Id).ToList());
            }
        }

        public void RemoveRange(IEnumerable<int> ids)
        {
            if (ids.Count() > 0)
            {
                this.RemoveRange(this.GetList(x => ids.Contains(x.Id)));
            }
        }

        public void Update(TEntity entity)
        {
            var type = entity.GetType();
            var lastUpdate = type.GetProperty("LastUpdate");
            if (lastUpdate != null)
            {
                lastUpdate.SetValue(entity, DateTime.Now, null);
            }

            var lastUpdater = type.GetProperty("LastUpdaterId");
            if (lastUpdater != null)
            {
                var user = this.Utils.GetCurrentUser();
                lastUpdater.SetValue(entity, user.Id, null);
            }

            this.Context.Set<TEntity>().Update(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), entity.Id);
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("update"), entity.Id);
            }
        }

        public void Update<T>(int id, T entity)
        {
            var originalEntity = this.Get(id);

            if (originalEntity == null)
            {
                var user = this.Utils.GetCurrentUser();
                throw this.Logger.LogInvalidThings(user, this.GetService(), "id", this.GetEntityErrorMessage());
            }

            this.Mapper.Map(entity, originalEntity);

            this.Update(originalEntity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().UpdateRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), entities.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("update"), entities.Select(x => x.Id).ToList());
            }
        }

        public void UpdateRange<T>(Dictionary<int, T> entities)
        {
            foreach (var i in entities.Keys)
            {
                this.Update<T>(i, entities[i]);
            }
        }

        private string GetService()
        {
            return $"{nameof(TEntity)} Service";
        }

        private string GetEvent(string action)
        {
            return $"{action} {nameof(TEntity)}";
        }

        private string GetEntityErrorMessage()
        {
            return $"{nameof(TEntity)} does not exist";
        }
    }
}