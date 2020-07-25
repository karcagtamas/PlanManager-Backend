using System.Collections.Generic;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface INewsService
    {
        List<NewsDto> GetNewsPosts();
        void PostNews(PostModel model);
        void UpdateNews(int postId, PostModel model);
        void DeleteNews(int postId);
    }
}