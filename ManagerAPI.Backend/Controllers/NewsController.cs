﻿using System;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    /// <summary>
    /// News Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : MyController<News, PostModel, NewsListDto, NewsDto, SystemNotificationType>
    {
        private readonly INewsService _newsService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="newsService">News Service</param>
        /// <param name="loggerService">Utils Service</param>
        public NewsController(INewsService newsService, ILoggerService loggerService):base(loggerService, newsService)
        {
            _newsService = newsService;
        }
    }
}