using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PlanManager.DataAccess;
using PlanManager.Services.DTOs.EM;
using PlanManager.Services.Messages.EM;

namespace PlanManager.Services.Services.EM
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly EventMessages _eventMessages;

        public EventService(DatabaseContext context, IUtilsService utilsService, IMapper mapper)
        {
            _context = context;
            _utilsService = utilsService;
            _mapper = mapper;
            _eventMessages = new EventMessages();
        }
        
        public List<MyEventListDto> GetMyEvents()
        {
            var user = _utilsService.GetCurrentUser();
            var list = _context.UserEventsSwitch.Where(x => x.User.Id == user.Id).Select(x => x.Event).ToList();
            var dtoList = _mapper.Map<List<MyEventListDto>>(list);
            _utilsService.LogInformation(_eventMessages.AllPlansGet, user);
            return dtoList;
        }
    }
}