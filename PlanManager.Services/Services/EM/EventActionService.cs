using System.Collections.Generic;
using AutoMapper;
using PlanManager.DataAccess;
using PlanManager.Services.DTOs.EM;
using System.Linq;
using PlanManager.Services.Messages.EM;

namespace PlanManager.Services.Services.EM
{
    public class EventActionService : IEventActionService
    {
        private readonly IUtilsService _utilsService;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly EventMessages _messages;

        public EventActionService(IUtilsService utilsService, DatabaseContext context, IMapper mapper)
        {
            _utilsService = utilsService;
            _context = context;
            _mapper = mapper;
            _messages = new EventMessages();
        }
        
        public List<EventActionListDto> GetActions(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var list = _context.MasterEvents.Find(eventId).Actions.ToList();
            var dtoList = _mapper.Map<List<EventActionListDto>>(list);
            _utilsService.LogInformation(_messages.EventActionsGet, user);
            return dtoList;
        }
    }
}