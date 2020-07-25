using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class WorkingDayService : Repository<WorkingDay>, IWorkingDayService
    {
        // Actions
        private const string GetWorkingDayAction = "get working day";
        private const string GetWorkingDayStatAction = "get working day stat";

        // Thing
        private const string DayThing = "day";

        // Message
        private const string WorkingDayDoesNotExistMessage = "Working day does not exist";

        // Injects
        private readonly DatabaseContext DatabaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingDayService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper)
        {
            this.DatabaseContext = context;
        }

        public WorkingDayListDto Get(DateTime day)
        {
            User user = this.Utils.GetCurrentUser();
            var workingDay = user.WorkingDays.FirstOrDefault(x => x.Day == day);

            if (workingDay == null)
            {
                throw this.Logger.LogInvalidThings(user, nameof(WorkingDayService), DayThing, WorkingDayDoesNotExistMessage);
            }

            var dto = this.Mapper.Map<WorkingDayListDto>(workingDay);
            this.Logger.LogInformation(user, nameof(WorkingDayService), GetWorkingDayAction, dto.Id);
            return dto;
        }

        public WorkingDayStatDto Stat(int id)
        {
            var user = this.Utils.GetCurrentUser();
            var workingDay = this.DatabaseContext.WorkingDays.Find(id);

            if (workingDay == null)
            {
                throw this.Logger.LogInvalidThings(user, nameof(WorkingDayService), DayThing, WorkingDayDoesNotExistMessage);
            }

            this.Logger.LogInformation(user, nameof(WorkingDayService), GetWorkingDayStatAction, workingDay.Id);

            return this.Mapper.Map<WorkingDayStatDto>(workingDay);
        }
    }
}
