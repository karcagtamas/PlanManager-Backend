using System.Collections.Generic;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Services
{
    public interface INewsService
    {
        List<NewsDto> GetNewsPosts();
        void PostNews(PostModel model);
        void UpdateNews(int postId, PostModel model);
        void DeleteNews(int postId);
    }
}