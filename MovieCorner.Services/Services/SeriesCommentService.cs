﻿using System.Collections.Generic;
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
    public class SeriesCommentService : Repository<SeriesComment, StatusLibraryNotificationType>, ISeriesCommentService
    {
        private readonly DatabaseContext _databaseContext;

        public SeriesCommentService(DatabaseContext context, ILoggerService logger, IUtilsService utils,
            INotificationService notification, IMapper mapper) : base(context, logger, utils, notification, mapper,
            "Series Comment", new NotificationArguments
            {
                DeleteArguments = new List<string>(),
                UpdateArguments = new List<string>(),
                CreateArguments = new List<string>()
            })
        {
            this._databaseContext = context;
        }

        public List<SeriesCommentListDto> GetList(int seriesId)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this._databaseContext.Series.Find(seriesId);

            var list = series?.Comments != null
                ? this.Mapper.Map<List<SeriesCommentListDto>>(series.Comments).Select(x =>
                {
                    x.OwnerIsCurrent = x.UserId == (user?.Id ?? "");
                    return x;
                }).OrderBy(x => x.Creation).ToList()
                : new List<SeriesCommentListDto>();


            return list;
        }
    }
}