using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces {
    public interface INewsService {
        Task<List<NewsDto>> GetNewsPosts ();
        Task<bool> PostNews (PostModel model);
        Task<bool> UpdateNews (int postId, PostModel model);
        Task<bool> DeleteNews (int postId);
    }
}