using ManagerAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context) 
        {
            this.Context = context;
        }

        public void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().AddRange(entities);
        }

        public TEntity Get(int id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        public TEntity Get(string id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
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

        public void Remove(TEntity entity)
        {
            this.Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            this.Context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            this.Context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
