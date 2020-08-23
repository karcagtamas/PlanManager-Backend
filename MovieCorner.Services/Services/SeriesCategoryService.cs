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
    public class SeriesCategoryService: Repository<SeriesCategory, StatusLibraryNotificationType>, ISeriesCategoryService
    {
        private readonly DatabaseContext _databaseContext;

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

        public List<SeriesCategorySelectorListDto> GetSelectorList(int seriesId)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<SeriesCategorySelectorListDto>();
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