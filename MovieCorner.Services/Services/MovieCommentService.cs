using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class MovieCommentService : Repository<MovieComment, StatusLibraryNotificationType>, IMovieCommentService
    {
        public MovieCommentService(DatabaseContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Movie Comment", new NotificationArguments
            {
                DeleteArguments = new List<string>(), UpdateArguments = new List<string>(),
                CreateArguments = new List<string>()
            })
        {
        }
    }
}