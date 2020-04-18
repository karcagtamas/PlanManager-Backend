using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities.EM;
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
            _utilsService.LogInformation(_eventMessages.AllMyEventGet, user);
            return dtoList;
        }

        public EventDto GetEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var eventEl = _context.MasterEvents.Find(eventId);
            if (eventEl == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            var eventDto = _mapper.Map<EventDto>(eventEl);
            _utilsService.LogInformation(_eventMessages.EventGet, user);
            return eventDto;
        }

        public void CreateEvent(EventCreateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _mapper.Map<MasterEvent>(model);
            _context.MasterEvents.Add(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.MasterEventCreate, user);
        }

        public void SetEventAsGtEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var gtEvent = new DGtEvent { EventId = eventId };
            _context.DGtEvents.Add(gtEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DGtEventCreate, user);
        }

        public void SetEventAsSportEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var sportEvent = new DSportEvent { EventId = eventId };
            _context.DSportEvents.Add(sportEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DSportEventCreate, user);
        }

        public void UpdateMasterEvent(MasterEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(model.Id);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, masterEvent);
            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.MasterEventUpdate, user);
        }

        public void UpdateSportEvent(SportEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.DSportEvents.Find(model.Id);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, masterEvent);
            _context.DSportEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DSportEventUpdate, user);
        }

        public void UpdateGtEvent(GtEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.DGtEvents.Find(model.Id);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, masterEvent);
            _context.DGtEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DGtEventUpdate, user);
        }

        public void DeleteEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _context.MasterEvents.Remove(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventDelete, user);
        }

        public void SetEventLockedStatus(int eventId, bool status)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsLocked = status;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsLockedStatusUpdate, user);
        }

        public void SetEventPublicStatus(int eventId, bool status)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsPublic = status;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsPublicStatusUpdate, user);
        }

        public void SetEventDisabledStatus(int eventId, bool status)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsDisabled = status;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsDisabledStatusUpdate, user);
        }
    }
}