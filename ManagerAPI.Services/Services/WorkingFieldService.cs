using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Services
{
    public class WorkingFieldService : Repository<WorkingField>, IWorkingFieldService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingFieldService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper)
        {
            this._databaseContext = context;
        }

        public WorkingWeekStatDto GetWeekStat(DateTime week)
        {
            return this.Mapper.Map<WorkingWeekStatDto>(this.GetList(x => x.WorkingDay.Day >= week && x.WorkingDay.Day <= week.AddDays(7)));
        }

        public WorkingMonthStatDto GetMonthStat(int year, int month)
        {
            return this.Mapper.Map<WorkingMonthStatDto>(this.GetList(x =>
                x.WorkingDay.Day.Year == year && x.WorkingDay.Day.Month == month));
        }
    }
}
