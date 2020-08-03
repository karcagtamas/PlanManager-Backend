using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces
{
    public interface INewsService : IHttpCall<NewsListDto, NewsDto, PostModel>
    {
    }
}