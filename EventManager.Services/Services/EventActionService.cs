using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.EM;

namespace EventManager.Services.Services
{
    /// <summary>
    /// Event Action Service
    /// </summary>
    public class EventActionService : IEventActionService
    {
        // Actions
        private const string GetEventActionsAction = "get event actions";

        // Things
        private const string EventIdThing = "event id";

        // Messages
        private const string EventDoesNotExistMessage = "Event does not exist";

        // Injects
        private readonly IUtilsService _utilsService;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="loggerService">Logger Service</param>
        public EventActionService(IUtilsService utilsService, DatabaseContext context, IMapper mapper, ILoggerService loggerService)
        {
            _utilsService = utilsService;
            _context = context;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        
        /// <summary>
        /// Get event actions
        /// </summary>
        /// <param name="eventId">Event's Id</param>
        /// <returns>List of actions</returns>
        public List<EventActionListDto> GetActions(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);

            if (masterEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            var list = masterEvent.Actions.ToList();
            var dtoList = _mapper.Map<List<EventActionListDto>>(list);
            _loggerService.LogInformation(user, nameof(EventActionService), GetEventActionsAction, dtoList.Select(x => x.Id).ToList());
            return dtoList;
        }
    }
}