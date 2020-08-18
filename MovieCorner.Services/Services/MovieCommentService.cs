using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class MovieCommentService : Repository<MovieComment, StatusLibraryNotificationType>, IMovieCommentService
    {
        private readonly DatabaseContext _databaseContext;

        public MovieCommentService(DatabaseContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Movie Comment", new NotificationArguments
            {
                DeleteArguments = new List<string>(),
                UpdateArguments = new List<string>(),
                CreateArguments = new List<string>()
            })
        {
            this._databaseContext = context;
        }

        public List<MovieCommentListDto> GetList(int movieId)
        {
            var user = this.Utils.GetCurrentUser();

            var movie = this._databaseContext.Movies.Find(movieId);

            return movie != null && movie.Comments != null ? this.Mapper.Map<List<MovieCommentListDto>>(movie.Comments) : new List<MovieCommentListDto>();
        }
    }
}