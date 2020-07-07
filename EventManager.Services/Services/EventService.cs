using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventManager.Services.Messages;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.EM;
using ManagerAPI.Models.Entities;
using ManagerAPI.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly EventMessages _eventMessages;

        private readonly List<EventRole> EventRoles = new List<EventRole>
        {
            new EventRole
            {
                Title = "General Member",
                AccessLevel = 1
            },
            new EventRole
            {
                Title = "Reporter",
                AccessLevel = 2
            },
            new EventRole
            {
                Title = "Manager",
                AccessLevel = 3
            },
            new EventRole
            {
                Title = "Moderator",
                AccessLevel = 4
            },
            new EventRole
            {
                Title = "Administrator",
                AccessLevel = 5
            },
            new EventRole
            {
                Title = "Owner",
                AccessLevel = 6
            }
        };

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
            masterEvent.CreatorId = user.Id;
            masterEvent.LastUpdaterId = user.Id;
            _context.MasterEvents.Add(masterEvent);

            _context.SaveChanges();

            _context.UserEventsSwitch.Add(new UserEvent
            {
                EventId = masterEvent.Id,
                UserId = user.Id,
                AddedById = user.Id
            });

            _context.SaveChanges();

            foreach (var i in EventRoles)
            {
                var roleSwitch = new UserEventRole
                {
                    UserId = user.Id,
                    AddedById = user.Id,
                    Role = new EventRole
                    {
                        AccessLevel = i.AccessLevel,
                        Title = i.Title,
                        EventId = masterEvent.Id
                    }
                };
                _context.UserEventRolesSwitch.Add(roleSwitch);
                _context.SaveChanges();
            }
            _utilsService.LogInformation(_eventMessages.MasterEventCreate, user);
        }

        public void SetEventAsGtEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            var gtEvent = new DGtEvent {EventId = eventId};
            _context.DGtEvents.Add(gtEvent);

            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;
            _context.MasterEvents.Update(masterEvent);

            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DGtEventCreate, user);
        }

        public void SetEventAsSportEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            var sportEvent = new DSportEvent {EventId = eventId};
            _context.DSportEvents.Add(sportEvent);

            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;
            _context.MasterEvents.Update(masterEvent);

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
            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.MasterEventUpdate, user);
        }

        public void UpdateSportEvent(SportEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var sportEvent = _context.DSportEvents.Find(model.Id);
            if (sportEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, sportEvent);
            sportEvent.Event.LastUpdater = user;
            sportEvent.Event.LastUpdate = DateTime.Now;
            _context.DSportEvents.Update(sportEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.DSportEventUpdate, user);
        }

        public void UpdateGtEvent(GtEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var gtEvent = _context.DGtEvents.Find(model.Id);
            if (gtEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, gtEvent);
            gtEvent.Event.LastUpdater = user;
            gtEvent.Event.LastUpdate = DateTime.Now;
            _context.DGtEvents.Update(gtEvent);
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
    }
}