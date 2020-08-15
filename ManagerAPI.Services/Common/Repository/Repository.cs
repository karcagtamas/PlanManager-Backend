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
    public class Repository<TEntity, TNotificationType> : IRepository<TEntity> where TEntity : class, IEntity where TNotificationType : Enum
    {
        private readonly DbContext _context;
        private readonly NotificationArguments _arguments;
        protected readonly ILoggerService Logger;
        protected readonly IUtilsService Utils;
        protected readonly INotificationService Notification;
        protected readonly IMapper Mapper;
        protected readonly string Entity;

        protected Repository(DbContext context, ILoggerService logger, IUtilsService utils, INotificationService notification, IMapper mapper, string entity, NotificationArguments arguments)
        {
            this._context = context;
            this.Logger = logger;
            this.Utils = utils;
            this.Notification = notification;
            this.Mapper = mapper;
            this.Entity = entity;
            this._arguments = arguments;
        }

        public void Add(TEntity entity)
        {
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

            this._context.Set<TEntity>().Add(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add"), entity.Id);

                List<string> args = this.DetermineArguments(this._arguments.CreateArguments, type, entity, user);

                this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user, args.ToArray());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("add"), entity.Id);
            }
        }

        public void Add<T>(T entity)
        {
            this.Add(this.Mapper.Map<TEntity>(entity));
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
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

                return x;
            });

            this._context.Set<TEntity>().AddRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add"), entities.Select(x => x.Id).ToList());

                foreach (var entity in entities)
                {
                    var type = entity.GetType();

                    List<string> args = this.DetermineArguments(this._arguments.CreateArguments, type, entity, user);

                    this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user, args.ToArray());
                }
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("add"), entities.Select(x => x.Id).ToList());
            }
        }

        public void AddRange<T>(IEnumerable<T> entities)
        {
            this.AddRange(this.Mapper.Map<IEnumerable<TEntity>>(entities));
        }

        public void Complete()
        {
            this._context.SaveChanges();
        }

        public TEntity Get(params object[] keys)
        {
            var entity = this._context.Set<TEntity>().Find(keys);

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

        public T Get<T>(params object[] keys)
        {
            return this.Mapper.Map<T>(this.Get(keys));
        }

        public List<TEntity> GetAll()
        {
            var list = this._context.Set<TEntity>().ToList();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), list.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"), list.Select(x => x.Id).ToList());
            }

            return list;
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
            var query = this._context.Set<TEntity>().Where(predicate);

            if (count != null)
            {
                query = query.Take((int)count);
            }

            if (skip != null)
            {
                query = query.Skip((int)skip);
            }

            var list = query.ToList();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), list.Select(x => x.Id).ToList());
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("get"), list.Select(x => x.Id).ToList());
            }

            return list;
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

            this._context.Set<TEntity>().Remove(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"), entity.Id);

                this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("delete"), true), user, args.ToArray());
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

            this._context.Set<TEntity>().RemoveRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete"), entities.Select(x => x.Id).ToList());

                foreach (var entity in args)
                {
                    this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("delete"), true), user, entity.Value.ToArray());
                }
            }
            catch (Exception)
            {
                this.Logger.LogAnonimInformation(this.GetService(), this.GetEvent("delete"), entities.Select(x => x.Id).ToList());
            }
        }

        public void RemoveRange(IEnumerable<int> ids)
        {
            if (ids.Any())
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

            this._context.Set<TEntity>().Update(entity);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), entity.Id);

                List<string> args = this.DetermineArguments(this._arguments.UpdateArguments, type, entity, user);

                this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("update"), true), user, args.ToArray());
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
            this._context.Set<TEntity>().UpdateRange(entities);

            this.Complete();

            try
            {
                var user = this.Utils.GetCurrentUser();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), entities.Select(x => x.Id).ToList());

                foreach (var entity in entities)
                {
                    var type = entity.GetType();

                    List<string> args = this.DetermineArguments(this._arguments.UpdateArguments, type, entity, user);

                    this.Notification.AddNotificationByType(typeof(TNotificationType), Enum.Parse(typeof(TNotificationType), this.GetNotificationAction("add"), true), user, args.ToArray());
                }
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

        protected string GetService()
        {
            return $"{this.Entity} Service";
        }

        protected string GetEvent(string action)
        {
            return $"{action} {this.Entity}";
        }

        protected string GetEntityErrorMessage()
        {
            return $"{this.Entity} does not exist";
        }

        private string GetNotificationAction(string action)
        {
            return string.Join("", this.GetEvent(action).Split(" ").Select(x => char.ToUpper(x[0]) + x.Substring(1).ToLower()));
        }

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
                    if (propList[j] == "CurrentUser")
                    {
                        lastType = user.GetType();
                        lastEntity = user;
                    }
                    else
                    {
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

                if (lastEntity != null && lastType != null)
                {
                    if (lastType == typeof(string))
                    {
                        args.Add((string)lastEntity);
                    }
                    else if (lastType == typeof(DateTime))
                    {
                        args.Add(((DateTime)lastEntity).ToLongDateString());
                    }
                    else if (lastType == typeof(int))
                    {
                        args.Add(((int)lastEntity).ToString());
                    }
                    else if (lastType == typeof(decimal))
                    {
                        args.Add(((decimal)lastEntity).ToString(CultureInfo.CurrentCulture));
                    }
                    else if (lastType == typeof(double))
                    {
                        args.Add(((double)lastEntity).ToString(CultureInfo.CurrentCulture));
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
    }
}