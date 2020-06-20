using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class NewsService : INewsService
    {
        private DatabaseContext Context { get; }
        private IUtilsService UtilsService { get; }
        private INotificationService NotificationService { get; }
        private IMapper Mapper { get; }

        public NewsService(DatabaseContext context, IUtilsService utilsService, INotificationService notificationService, IMapper mapper)
        {
            Context = context;
            UtilsService = utilsService;
            NotificationService = notificationService;
            Mapper = mapper;
        }

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

        public List<NewsDto> GetNewsPosts()
        {
            var user = UtilsService.GetCurrentUser();

            var list = Mapper.Map<List<NewsDto>>(Context.News.ToList());

            UtilsService.LogInformation(NewsMessages.NewsGet, user);

            return list;
        }

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

        public void UpdateNews(PostModel model)
        {
            var user = UtilsService.GetCurrentUser();

            var news = Context.News.Find(model.Id);

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
