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
    public class MovieCategoryService : Repository<MovieCategory, StatusLibraryNotificationType>, IMovieCategoryService
    {
        private readonly DatabaseContext _databaseContext;

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

        public List<MovieCategorySelectorListDto> GetSelectorList(int movieId)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<MovieCategorySelectorListDto>();
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