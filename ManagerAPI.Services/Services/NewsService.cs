using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Messages;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// News Service
    /// </summary>
    public class NewsService : INewsService
    {
        private DatabaseContext Context { get; }
        private IUtilsService UtilsService { get; }
        private INotificationService NotificationService { get; }
        private IMapper Mapper { get; }

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        public NewsService(DatabaseContext context, IUtilsService utilsService, INotificationService notificationService, IMapper mapper)
        {
            Context = context;
            UtilsService = utilsService;
            NotificationService = notificationService;
            Mapper = mapper;
        }

        /// <summary>
        /// Delete News by Id
        /// </summary>
        /// <param name="postId">News Id</param>
        public void DeleteNews(int postId)
        {
            var user = UtilsService.GetCurrentUser();

            var news = Context.News.Find(postId);
            var creator = news.Creator;

            if (news == null)
            {
                throw new Exception(NewsMessages.InvalidNews);
            }

            Context.News.Remove(news);
            Context.SaveChanges();

            UtilsService.LogInformation(NewsMessages.NewsRemove, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.NewsDeleted, creator);
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns>List of news</returns>
        public List<NewsDto> GetNewsPosts()
        {
            var user = UtilsService.GetCurrentUser();

            var list = Mapper.Map<List<NewsDto>>(Context.News.OrderBy(x => x.Creation).ToList());

            UtilsService.LogInformation(NewsMessages.NewsGet, user);

            return list;
        }

        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="model">Model of news for creation</param>
        public void PostNews(PostModel model)
        {
            var user = UtilsService.GetCurrentUser();

            var news = new News();
            news.Content = model.Content;
            news.CreatorId = user.Id;
            news.LastUpdaterId = user.Id;

            Context.News.Add(news);
            Context.SaveChanges();

            UtilsService.LogInformation(NewsMessages.NewsAdd, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.NewsAdded, user);
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="model">Model of news</param>
        public void UpdateNews(int postId, PostModel model)
        {
            var user = UtilsService.GetCurrentUser();

            var news = Context.News.Find(postId);

            if (news == null)
            {
                throw new Exception(NewsMessages.InvalidNews);
            }

            news.Content = model.Content;
            news.LastUpdaterId = user.Id;
            news.LastUpdate = DateTime.Now;

            Context.News.Update(news);
            Context.SaveChanges();

            UtilsService.LogInformation(NewsMessages.NewsUpdate, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.NewsUpdated, news.Creator);
        }
    }
}
