using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;

namespace MovieCorner.Services.Services
{
    public class EpisodeService : Repository<Episode>, IEpisodeService
    {
        // Injects
        private readonly DatabaseContext DatabaseContext;

        public EpisodeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper)
        {
            this.DatabaseContext = context;
        }
    }
}
