using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;

namespace MovieCorner.Services.Services
{
    public class SeasonService : Repository<Season>, ISeasonService
    {
        // Injects
        private readonly DatabaseContext DatabaseContext;

        public SeasonService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper, "Season")
        {
            this.DatabaseContext = context;
        }
    }
}
