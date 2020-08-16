using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class MovieCategoryService : Repository<MovieCategory, MovieCornerNotificationType>, IMovieCategoryService
    {
        protected MovieCategoryService(DbContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Movie Category", new NotificationArguments { })
        {
        }
    }
}