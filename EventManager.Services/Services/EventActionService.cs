using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.EM;
using ManagerAPI.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
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

        public EventActionService(IUtilsService utilsService, DatabaseContext context, IMapper mapper, ILoggerService loggerService)
        {
            _utilsService = utilsService;
            _context = context;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        
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