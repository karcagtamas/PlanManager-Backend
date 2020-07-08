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
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        public NewsService(DatabaseContext context, IUtilsService utilsService, INotificationService notificationService, IMapper mapper)
        {
            _context = context;
            _utilsService = utilsService;
            _notificationService = notificationService;
            _mapper = mapper;
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
                throw new Exception(NewsMessages.InvalidNews);
            }

            _context.News.Remove(news);
            _context.SaveChanges();

            _utilsService.LogInformation(NewsMessages.NewsRemove, user);
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

            _utilsService.LogInformation(NewsMessages.NewsGet, user);

            return list;
        }

        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="model">Model of news for creation</param>
        public void PostNews(PostModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var news = new News();
            news.Content = model.Content;
            news.CreatorId = user.Id;
            news.LastUpdaterId = user.Id;

            _context.News.Add(news);
            _context.SaveChanges();

            _utilsService.LogInformation(NewsMessages.NewsAdd, user);
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
                throw new Exception(NewsMessages.InvalidNews);
            }

            news.Content = model.Content;
            news.LastUpdaterId = user.Id;
            news.LastUpdate = DateTime.Now;

            _context.News.Update(news);
            _context.SaveChanges();

            _utilsService.LogInformation(NewsMessages.NewsUpdate, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.NewsUpdated, news.Creator);
        }
    }
}
