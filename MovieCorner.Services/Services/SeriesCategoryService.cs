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
    /// Series Category Series
    /// </summary>
    public class SeriesCategoryService : Repository<SeriesCategory, StatusLibraryNotificationType>,
        ISeriesCategoryService
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
        public SeriesCategoryService(DatabaseContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Series Category", new NotificationArguments
            {
                DeleteArguments = new List<string> {"Name"}, UpdateArguments = new List<string> {"Name"},
                CreateArguments = new List<string> {"Name"}
            })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Gets selector list for the given series.
        /// </summary>
        /// <param name="seriesId">Series Id</param>
        /// <returns>Series category selector list for the requested series.</returns>
        public List<SeriesCategorySelectorListDto> GetSelectorList(int seriesId)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<SeriesCategorySelectorListDto>().OrderBy(x => x.Name).ToList();
            var series = this._databaseContext.Series.FirstOrDefault(x => x.Id == seriesId);

            var selected = series != null
                ? series.Categories.Select(x => x.Category).ToList()
                : new List<SeriesCategory>();

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