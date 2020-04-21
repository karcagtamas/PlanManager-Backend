using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlanManager.DataAccess;
using PlanManager.DataAccess.Entities;
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
            var masterEvent = _context.DSportEvents.Find(model.Id);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            _mapper.Map(model, masterEvent);
            // TODO: Last updater
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
            // TODO: Last updater
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

        public void SetEventStatus(int eventId, string type, bool status)
        {
            var user = _utilsService.GetCurrentUser();
            switch (type)
            {
                case "public":
                    SetEventPublicStatus(eventId, status, user);
                    break;
                case "disable":
                    SetEventDisabledStatus(eventId, status, user);
                    break;
                case "lock":
                    SetEventLockedStatus(eventId, status, user);
                    break;
                default:
                    throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidStatusType, user));
            }
        }

        public void SetEventLockedStatus(int eventId, bool status, User user)
        {
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsLocked = status;
            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsLockedStatusUpdate, user);
        }

        public void SetEventPublicStatus(int eventId, bool status, User user)
        {
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsPublic = status;
            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsPublicStatusUpdate, user);
        }

        public void SetEventDisabledStatus(int eventId, bool status, User user)
        {
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_eventMessages.InvalidEventId, user));
            }

            masterEvent.IsDisabled = status;
            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            _utilsService.LogInformation(_eventMessages.EventIsDisabledStatusUpdate, user);
        }
    }
}