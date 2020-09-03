using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Movie Category Service
    /// </summary>
    public class MovieCategoryService : Repository<MovieCategory, StatusLibraryNotificationType>, IMovieCategoryService
    {
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="logger">Logger Service</param>
        /// <param name="utils">Utils Service</param>
        /// <param name="notification">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        public MovieCategoryService(DatabaseContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Movie Category", new NotificationArguments
            {
                DeleteArguments = new List<string> {"Name"}, UpdateArguments = new List<string> {"Name"},
                CreateArguments = new List<string> {"Name"}
            })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Gets selector list for the given movie.
        /// </summary>
        /// <param name="movieId">Movie Id</param>
        /// <returns>Movie category selector list for the requested movie.</returns>
        public List<MovieCategorySelectorListDto> GetSelectorList(int movieId)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<MovieCategorySelectorListDto>().OrderBy(x => x.Name).ToList();
            var movie = this._databaseContext.Movies.FirstOrDefault(x => x.Id == movieId);

            var selected = movie != null
                ? movie.Categories.Select(x => x.Category).ToList()
                : new List<MovieCategory>();

            foreach (var t in list)
            {
                t.IsSelected = selected.Any(x => x.Id == t.Id);
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get selector"),
                list.Select(x => x.Id).ToList());

            return list;
        }
    }
}