using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// News Service
    /// </summary>
    public class NewsService : INewsService
    {
        // Actions
        private const string UpdateNewsAction = "update news";
        private const string CreateNewsAction = "create news";
        private const string GetNewsPostsAction = "get news posts";
        private const string DeleteNewsAction = "delete news";

        // Things
        private const string NewsThing = "news";
        private const string NewsIdThing = "news id";

        // Messages
        private const string NewsDoesNotExistMessage = "News does not exist";
        private const string ModelIsNotCorrectForNewsUpdating = "Model is not correct for news updating";
        private const string ModelIsNotCorrectForNewsCreating = "Model is not correct for news creating";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="loggerService">Logger Service</param>
        public NewsService(DatabaseContext context, IUtilsService utilsService, INotificationService notificationService, IMapper mapper, ILoggerService loggerService)
        {
            _context = context;
            _utilsService = utilsService;
            _notificationService = notificationService;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Delete News by Id
        /// </summary>
        /// <param name="postId">News Id</param>
        public void DeleteNews(int postId)
        {
            var user = _utilsService.GetCurrentUser();

            var news = _context.News.Find(postId);
            var creator = news.Creator;

            if (news == null)
            {
                throw _loggerService.LogInvalidThings(user, nameof(NewsService), NewsIdThing, NewsDoesNotExistMessage);
            }

            _context.News.Remove(news);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(NewsService), DeleteNewsAction, news.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.NewsDeleted, creator);
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns>List of news</returns>
        public List<NewsDto> GetNewsPosts()
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<NewsDto>>(_context.News.OrderBy(x => x.Creation).ToList());

            _loggerService.LogInformation(user, nameof(NewsService), GetNewsPostsAction, list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="model">Model of news for creation</param>
        public void PostNews(PostModel model)
        {
            var user = _utilsService.GetCurrentUser();

            if (model == null) {
                throw _loggerService.LogInvalidThings(user, nameof(NewsService), NewsThing, ModelIsNotCorrectForNewsCreating);
            }

            var news = new News();
            news.Content = model.Content;
            news.CreatorId = user.Id;
            news.LastUpdaterId = user.Id;

            _context.News.Add(news);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(NewsService), CreateNewsAction, news.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.NewsAdded, user);
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="model">Model of news</param>
        public void UpdateNews(int postId, PostModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var news = _context.News.Find(postId);

            if (news == null)
            {
                throw _loggerService.LogInvalidThings(user, nameof(NewsService), NewsThing, NewsDoesNotExistMessage);
            }

            if (model == null) {
                throw _loggerService.LogInvalidThings(user, nameof(NewsService), NewsThing, ModelIsNotCorrectForNewsUpdating);
            }

            news.Content = model.Content;
            news.LastUpdaterId = user.Id;
            news.LastUpdate = DateTime.Now;

            _context.News.Update(news);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(NewsService), UpdateNewsAction, news.Id);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.NewsUpdated, news.Creator);
        }
    }
}
