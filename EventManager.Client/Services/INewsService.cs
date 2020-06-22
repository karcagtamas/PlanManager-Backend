using EventManager.Client.Models;
using EventManager.Client.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface INewsService
    {
        Task<ApiResponseModel<List<NewsDto>>> GetNewsPosts();
        Task<ApiResponseModel<object>> PostNews(PostModel model);
        Task<ApiResponseModel<object>> UpdateNews(int postId, PostModel model);
        Task<ApiResponseModel<object>> DeleteNews(int postId);
    }
}