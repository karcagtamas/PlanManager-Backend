using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count, int? skip);
        TEntity Get(int id);
        TEntity Get(string id);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
