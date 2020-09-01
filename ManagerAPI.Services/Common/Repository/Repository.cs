using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagerAPI.Services.Common.Repository
{
    /// <summary>
    /// Repository manager
    /// </summary>
    /// <typeparam name="TEntity">Type of Entity</typeparam>
    /// <typeparam name="TNotificationType">Type of Notification Type</typeparam>
    public class Repository<TEntity, TNotificationType> : IRepository<TEntity>
        where TEntity : class, IEntity where TNotificationType : Enum
    {
        private readonly DbContext _context;
        private readonly NotificationArguments _arguments;
        protected readonly ILoggerService Logger;
        protected readonly IUtilsService Utils;
        protected readonly INotificationService Notification;
        protected readonly IMapper Mapper;
        protected readonly string Entity;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="logger">Logger Service</param>
        /// <param name="utils">Utils Service</param>
        /// <param name="notification">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="entity">Entity name</param>
        /// <param name="arguments">Notification arguments</param>
        protected Repository(DbContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper, string entity, NotificationArguments arguments)
        {
            this._context = context;
            this.Logger = logger;
            this.Utils = utils;
            this.Notification = notification;
            this.Mapper = mapper;
            this.Entity = entity;
            this._arguments = arguments;
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Add(TEntity entity)
        {
            // Add default datas automatically
            var type = entity.GetType();
            var creator = type.GetProperty("CreatorId");
            if (creator != null)
            {
                var user = this.Utils.GetCurrentUser();
                creator.SetValue(entity, user.Id, null);
            }

            var lastUpdater = type.GetProperty("LastUpdaterId");
            if (lastUpdater != null)
            {
                var user = this.Utils.GetCurrentUser();
                lastUpdater.SetValue(entity, user.Id, null);
            }

            var userField = type.GetProperty("UserId");
            if (userField != null)
            {
                var user = this.Utils.GetCurrentUser();
                userField.SetValue(entity, user.Id, null);
            }

            var owner = type.GetProperty("OwnerId");
            if (owner != null)
            {
                var user = this.Utils.GetCurrentUser();
                owner.SetValue(entity, user.Id, null);
            }

            // Add
            this._context.Set<TEntity>().Add(entity);

            // Save
            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add"), entity.Id);

                // Notification generate
                List<string> args = this.DetermineArguments(this._arguments.CreateArguments, type, entity, user);

                this.Notification.AddNotificationByType(typeof(TNotificationType),
                    Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user,
                    args.ToArray());
            }
            catch (Exception)
            {
                // Anonim log
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("add"), entity.Id);
            }
        }

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <typeparam name="T">Type of mappable entity</typeparam>
        public void Add<T>(T entity)
        {
            this.Add(this.Mapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Add multiple entity.
        /// </summary>
        /// <param name="entities">Entities</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            // Add default datas automatically
            entities = entities.Select(x =>
            {
                var type = x.GetType();
                var creator = type.GetProperty("CreatorId");
                if (creator != null)
                {
                    var user = this.Utils.GetCurrentUser();
                    creator.SetValue(x, user.Id, null);
                }

                var lastUpdater = type.GetProperty("LastUpdaterId");
                if (lastUpdater != null)
                {
                    var user = this.Utils.GetCurrentUser();
                    lastUpdater.SetValue(x, user.Id, null);
                }

                var userField = type.GetProperty("UserId");
                if (userField != null)
                {
                    var user = this.Utils.GetCurrentUser();
                    userField.SetValue(x, user.Id, null);
                }

                var owner = type.GetProperty("OwnerId");
                if (owner != null)
                {
                    var user = this.Utils.GetCurrentUser();
                    owner.SetValue(x, user.Id, null);
                }

                    return x;
                });

            // Add
            this._context.Set<TEntity>().AddRange(entities);

            // Save
            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add"),
                    entities.Select(x => x.Id).ToList());

                // Notification generate
                foreach (var entity in entities)
                {
                    var type = entity.GetType();

                    List<string> args = this.DetermineArguments(this._arguments.CreateArguments, type, entity, user);

                    this.Notification.AddNotificationByType(typeof(TNotificationType),
                        Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user,
                        args.ToArray());
                }
            }
            catch (Exception)
            {
                // Anonim log
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("add"),
                    entities.Select(x => x.Id).ToList());
            }
        }

        /// <summary>
        /// Add multiple entity
        /// </summary>
        /// <param name="entities">Entites</param>
        /// <typeparam name="T">Type of mappable entities</typeparam>
        public void AddRange<T>(IEnumerable<T> entities)
        {
            this.AddRange(this.Mapper.Map<IEnumerable<TEntity>>(entities));
        }

        /// <summary>
        /// Save changes
        /// </summary>
        public void Complete()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="keys">Identity keys of entity</param>
        /// <returns>Entity with the given keys</returns>
        public TEntity Get(params object[] keys)
        {
            // Get
            var entity = this._context.Set<TEntity>().Find(keys);

            // Log
            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), entity.Id);
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"), entity.Id);
            }

            return entity;
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="keys">Identity keys of entity</param>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped entity to the destination type</returns>
        public T Get<T>(params object[] keys)
        {
            return this.Mapper.Map<T>(this.Get(keys));
        }

        /// <summary>
        /// Get all entity
        /// </summary>
        /// <returns>All existing entity</returns>
        public List<TEntity> GetAll()
        {
            // Get
            var list = this._context.Set<TEntity>().ToList();

            // Log
            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"),
                    list.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"),
                    list.Select(x => x.Id).ToList());
            }

            return list;
        }

        /// <summary>
        /// Get all entity.
        /// </summary>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped entity list to the destination type</returns>
        public List<T> GetAll<T>()
        {
            return this.Mapper.Map<List<T>>(this.GetAll());
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <returns>Filtered list of entities</returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetList(predicate, null, null);
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="count">Max result count.</param>
        /// <returns>Filtered list of entities with max count.</returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count)
        {
            return this.GetList(predicate, count, null);
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="count">Max result count.</param>
        /// <param name="skip">Skipped element number.</param>
        /// <returns>Filtered list of entities with max count and first skip.</returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int? count, int? skip)
        {
            // Get
            var query = this._context.Set<TEntity>().Where(predicate);

            // Count
            if (count != null)
            {
                query = query.Take((int) count);
            }

            // Skip
            if (skip != null)
            {
                query = query.Skip((int) skip);
            }

            // To list
            var list = query.ToList();

            // Log
            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"),
                    list.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"),
                    list.Select(x => x.Id).ToList());
            }

            return list;
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped filtered list of entities to destination type</returns>
        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate));
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="count">Max result count.</param>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped filtered list of entities with max count to destination type</returns>
        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate, int? count)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate, count));
        }

        /// <summary>
        /// Get list of entities.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="count">Max result count.</param>
        /// <param name="skip">Skipped element number.</param>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped filtered list of entities with max count and first skip to destination type</returns>
        public List<T> GetList<T>(Expression<Func<TEntity, bool>> predicate, int? count, int? skip)
        {
            return this.Mapper.Map<List<T>>(this.GetList(predicate, count, skip));
        }

        /// <summary>
        /// Remove entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Remove(TEntity entity)
        {
            // Determine Arguments
            List<string> args = new List<string>();
            try
            {
                var user = this.Utils.GetCurrentUser();

                var type = entity.GetType();
                args = this.DetermineArguments(this._arguments.DeleteArguments, type, entity, user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                args = new List<string>();
            }

            // Remove
            this._context.Set<TEntity>().Remove(entity);

            // Save
            this.Complete();
            
            try
            {
                var user = this.Utils.GetCurrentUser();
                
                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"), entity.Id);

                // Notification generate
                this.Notification.AddNotificationByType(typeof(TNotificationType),
                    Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("delete"), true), user,
                    args.ToArray());
            }
            catch (Exception)
            {
                // Log anonimus
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("delete"), entity.Id);
            }
        }

        /// <summary>
        /// Remove by Id
        /// </summary>
        /// <param name="id">Id of entity</param>
        public void Remove(int id)
        {
            // Get entity
            var entity = this.Get(id);
            
            if (entity == null)
            {
                var user = this.Utils.GetCurrentUser();
                throw this.Logger.LogInvalidThings(user, this.GetService(), "id", this.GetEntityErrorMessage());
            }

            // Remove
            this.Remove(this.Get(id));
        }

        /// <summary>
        /// Remove range
        /// </summary>
        /// <param name="entities">Entities</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            // Determine arguments
            var type = entities.ToList()[0].GetType();
            Dictionary<TEntity, List<string>> args = new Dictionary<TEntity, List<string>>();
            foreach (var entity in entities)
            {
                try
                {
                    var user = this.Utils.GetCurrentUser();
                    args.Add(entity, this.DetermineArguments(this._arguments.DeleteArguments, type, entity, user));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    args.Add(entity, new List<string>());
                }
            }

            // Remove range
            this._context.Set<TEntity>().RemoveRange(entities);

            // Save
            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"),
                    entities.Select(x => x.Id).ToList());

                // Notification generate
                foreach (var entity in args)
                {
                    this.Notification.AddNotificationByType(typeof(TNotificationType),
                        Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("delete"), true), user,
                        entity.Value.ToArray());
                }
            }
            catch (Exception)
            {
                // Anonim log
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("delete"),
                    entities.Select(x => x.Id).ToList());
            }
        }

        /// <summary>
        /// Remove range by Id
        /// </summary>
        /// <param name="ids">List of Ids</param>
        public void RemoveRange(IEnumerable<int> ids)
        {
            // Remove
            if (ids.Any())
            {
                this.RemoveRange(this.GetList(x => ids.Contains(x.Id)));
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(TEntity entity)
        {
            // Set default update data
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

            // Update
            this._context.Set<TEntity>().Update(entity);

            // Save
            this.Complete();
            
            try
            {
                var user = this.Utils.GetCurrentUser();

                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), entity.Id);

                // Notification generate
                List<string> args = this.DetermineArguments(this._arguments.UpdateArguments, type, entity, user);

                this.Notification.AddNotificationByType(typeof(TNotificationType),
                    Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("update"), true), user,
                    args.ToArray());
            }
            catch (Exception)
            {
                // Anonim log
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("update"), entity.Id);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <param name="entity">Entity model object</param>
        /// <typeparam name="T">Mappable type</typeparam>
        public void Update<T>(int id, T entity)
        {
            // Get original
            var originalEntity = this.Get(id);

            if (originalEntity == null)
            {
                var user = this.Utils.GetCurrentUser();
                throw this.Logger.LogInvalidThings(user, this.GetService(), "id", this.GetEntityErrorMessage());
            }

            // Update model to original entity
            this.Mapper.Map(entity, originalEntity);

            // Update
            this.Update(originalEntity);
        }

        /// <summary>
        /// Update multiple entity
        /// </summary>
        /// <param name="entities">Entities</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            // Update 
            this._context.Set<TEntity>().UpdateRange(entities);

            // Save
            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                // Log
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"),
                    entities.Select(x => x.Id).ToList());

                // Notification generate
                foreach (var entity in entities)
                {
                    var type = entity.GetType();

                    List<string> args = this.DetermineArguments(this._arguments.UpdateArguments, type, entity, user);

                    this.Notification.AddNotificationByType(typeof(TNotificationType),
                        Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user,
                        args.ToArray());
                }
            }
            catch (Exception)
            {
                // Anonim log
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("update"),
                    entities.Select(x => x.Id).ToList());
            }
        }

        /// <summary>
        /// Update multiple entity
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <typeparam name="T">Mappable type</typeparam>
        public void UpdateRange<T>(Dictionary<int, T> entities)
        {
            // Update
            foreach (var i in entities.Keys)
            {
                this.Update<T>(i, entities[i]);
            }
        }
        
        /// <summary>
        /// Generate entity service
        /// </summary>
        /// <returns>Entity Service name</returns>
        protected string GetService()
        {
            return $"{this.Entity} Service";
        }

        /// <summary>
        /// Generate event from action
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns>Event name</returns>
        protected string GetEvent(string action)
        {
            return $"{action} {this.Entity}";
        }

        /// <summary>
        /// Generate entity error message
        /// </summary>
        /// <returns>Error message</returns>
        protected string GetEntityErrorMessage()
        {
            return $"{this.Entity} does not exist";
        }

        /// <summary>
        /// Generate notification action from action
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns>Notification action</returns>
        private string GetNotificationAction(string action)
        {
            return string.Join("",
                this.GetEvent(action).Split(" ").Select(x => char.ToUpper(x[0]) + x.Substring(1).ToLower()));
        }

        /// <summary>
        /// Determine arguments from entity by name
        /// </summary>
        /// <param name="nameList">Name list</param>
        /// <param name="firstType">First entity's type</param>
        /// <param name="entity">Entity</param>
        /// <param name="user">User</param>
        /// <returns>Declared argument value list</returns>
        private List<string> DetermineArguments(List<string> nameList, Type firstType, TEntity entity, User user)
        {
            List<string> args = new List<string>();

            foreach (var i in nameList)
            {
                var propList = i.Split(".");
                var lastType = firstType;
                object lastEntity = entity;

                for (int j = 0; j < propList.Count(); j++)
                {
                    // Special event for updater
                    if (propList[j] == "CurrentUser")
                    {
                        lastType = user.GetType();
                        lastEntity = user;
                    }
                    else
                    {
                        // Get inner entity from entity
                        var prop = lastType.GetProperty(propList[j]);
                        if (prop != null)
                        {
                            lastEntity = prop.GetValue(lastEntity);
                            if (lastEntity != null)
                            {
                                lastType = lastEntity.GetType();
                            }
                        }
                    }
                }

                // Last entity is primitive (writeable)
                if (lastEntity != null && lastType != null)
                {
                    if (lastType == typeof(string))
                    {
                        args.Add((string) lastEntity);
                    }
                    else if (lastType == typeof(DateTime))
                    {
                        args.Add(((DateTime) lastEntity).ToLongDateString());
                    }
                    else if (lastType == typeof(int))
                    {
                        args.Add(((int) lastEntity).ToString());
                    }
                    else if (lastType == typeof(decimal))
                    {
                        args.Add(((decimal) lastEntity).ToString(CultureInfo.CurrentCulture));
                    }
                    else if (lastType == typeof(double))
                    {
                        args.Add(((double) lastEntity).ToString(CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        args.Add("");
                    }
                }
                else
                {
                    args.Add("");
                }
            }

            return args;
        }

        /// <summary>
        /// Get ordered list
        /// </summary>
        /// <param name="orderBy">Ordering by</param>
        /// <param name="direction">Order direction</param>
        /// <returns>Ordered all list</returns>
        public List<TEntity> GetOrderedAll(string orderBy, string direction)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var type = typeof(TEntity);
                var property = type.GetProperty(orderBy);

                if (property == null)
                {
                    throw new ArgumentException("Property does not exist");
                }
                
                switch (direction)
                {
                    case "asc":
                        return this.GetAll().OrderBy(x => property.GetValue(x)).ToList();
                    case "desc":
                        return this.GetAll().OrderByDescending(x => property.GetValue(x)).ToList();
                    case "none":
                        return this.GetAll();
                    default: throw new ArgumentException("Ordering direction does not exist");
                }
            }

            throw new ArgumentException("Order by value is empty or null");
        }

        /// <summary>
        /// Get ordered list
        /// </summary>
        /// <param name="orderBy">Order by</param>
        /// <param name="direction">Order direction</param>
        /// <typeparam name="T">Type of mappable result type</typeparam>
        /// <returns>Mapped and ordered list</returns>
        public List<T> GetOrderedAll<T>(string orderBy, string direction)
        {
            return this.Mapper.Map<List<T>>(this.GetOrderedAll(orderBy, direction));
        }
    }
}