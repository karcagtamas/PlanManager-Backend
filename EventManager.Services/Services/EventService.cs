using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.EM;
using ManagerAPI.Models.Entities.EM;
using ManagerAPI.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    /// <summary>
    /// Event Service
    /// </summary>
    public class EventService : IEventService
    {
        // Actions
        private const string DeleteEventAction = "delete event";
        private const string UpdateGtEventAction = "update gt event";
        private const string UpdateSportEventAction = "update sport event";
        private const string UpdateMessageEventAction = "update master event";
        private const string ExtendEventToSportAction = "extend event to sport";
        private const string ExtendEventToGtAction = "extend event to gt";
        private const string CreateEventAction = "create event";
        private const string GetEventsAction = "get events";
        private const string GetEventAction = "get event";

        // Things
        private const string EventIdThing = "event id";
        private const string GtEventIdThing = "gt event id";
        private const string SportEventIdThing = "sport event id";
        private const string OperationThing = "operation";

        // Messages
        private const string EventDoesNotExistMessage = "Event does not exist";
        private const string GtEventDoesNotExistMessage = "Gt event does not exist";
        private const string SportEventDoesNotExistMessage = "Sport event does not exist";
        private const string EventAlreadyOwnSportExtensionMessage = "Event already own sport extension";
        private const string EventAlreadyOwnGtExtensionMessage = "Event already own gt extension";

        // Inejcts
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

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

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="mapper">Mappper</param>
        /// <param name="loggerService">Logger Service</param>
        public EventService(DatabaseContext context, IUtilsService utilsService, IMapper mapper, ILoggerService loggerService)
        {
            _context = context;
            _utilsService = utilsService;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get Event list for the current user
        /// </summary>
        /// <returns>List of events</returns>
        public List<MyEventListDto> GetMyEvents()
        {
            var user = _utilsService.GetCurrentUser();
            var list = _context.UserEventsSwitch.Where(x => x.User.Id == user.Id).Select(x => x.Event).ToList();
            var dtoList = _mapper.Map<List<MyEventListDto>>(list);
            this._loggerService.LogInformation(user, nameof(EventService), GetEventsAction, list.Select(x => x.Id).ToList());
            return dtoList;
        }

        /// <summary>
        /// Get event by the given Id
        /// </summary>
        /// <param name="eventId">Id of the event</param>
        /// <returns>Event</returns>
        public EventDto GetEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var eventEl = _context.MasterEvents.Find(eventId);
            if (eventEl == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            var eventDto = _mapper.Map<EventDto>(eventEl);
            this._loggerService.LogInformation(user, nameof(EventService), GetEventAction, eventId);
            return eventDto;
        }

        /// <summary>
        /// Create event from the given model.
        /// Add user to this newly created event.
        /// Create roles for the event.
        /// </summary>
        /// <param name="model">Event model</param>
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
            this._loggerService.LogInformation(user, nameof(EventService), CreateEventAction, masterEvent.Id);
        }

        /// <summary>
        /// Evolve event to gt event
        /// </summary>
        /// <param name="eventId">Event's Id</param>
        public void SetEventAsGtEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            if (masterEvent.GtEvent != null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), OperationThing, EventAlreadyOwnGtExtensionMessage);
            }

            var gtEvent = new DGtEvent {EventId = eventId};
            _context.DGtEvents.Add(gtEvent);

            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;
            _context.MasterEvents.Update(masterEvent);

            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), ExtendEventToGtAction, $"{masterEvent.Id}-{gtEvent.Id}");
        }

        /// <summary>
        /// Evolve event to sport event
        /// </summary>
        /// <param name="eventId">Event's Id</param>
        public void SetEventAsSportEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            if (masterEvent.SportEvent != null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), OperationThing, EventAlreadyOwnSportExtensionMessage);
            }

            var sportEvent = new DSportEvent {EventId = eventId};
            _context.DSportEvents.Add(sportEvent);

            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;
            _context.MasterEvents.Update(masterEvent);

            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), ExtendEventToSportAction, $"{masterEvent.Id}-{sportEvent.Id}");
        }

        /// <summary>
        /// Update master event part
        /// </summary>
        /// <param name="model">Model of update</param>
        public void UpdateMasterEvent(MasterEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(model.Id);
            if (masterEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            _mapper.Map(model, masterEvent);
            masterEvent.LastUpdaterId = user.Id;
            masterEvent.LastUpdate = DateTime.Now;

            _context.MasterEvents.Update(masterEvent);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), UpdateMessageEventAction, masterEvent.Id);
        }

        /// <summary>
        /// Update sport event part
        /// </summary>
        /// <param name="model">Model of update</param>
        public void UpdateSportEvent(SportEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var sportEvent = _context.DSportEvents.Find(model.Id);
            if (sportEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), SportEventIdThing, SportEventDoesNotExistMessage);
            }

            _mapper.Map(model, sportEvent);
            sportEvent.Event.LastUpdater = user;
            sportEvent.Event.LastUpdate = DateTime.Now;
            _context.DSportEvents.Update(sportEvent);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), UpdateSportEventAction, sportEvent.Id);
        }

        /// <summary>
        /// Update gt event part
        /// </summary>
        /// <param name="model">Model of update</param>
        public void UpdateGtEvent(GtEventUpdateDto model)
        {
            var user = _utilsService.GetCurrentUser();
            var gtEvent = _context.DGtEvents.Find(model.Id);
            if (gtEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), GtEventIdThing, GtEventDoesNotExistMessage);
            }

            _mapper.Map(model, gtEvent);
            gtEvent.Event.LastUpdater = user;
            gtEvent.Event.LastUpdate = DateTime.Now;
            _context.DGtEvents.Update(gtEvent);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), UpdateGtEventAction, gtEvent.Id);
        }

        /// <summary>
        /// Delete event by the given Id
        /// </summary>
        /// <param name="eventId">Event's Id</param>
        public void DeleteEvent(int eventId)
        {
            var user = _utilsService.GetCurrentUser();
            var masterEvent = _context.MasterEvents.Find(eventId);
            if (masterEvent == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(EventService), EventIdThing, EventDoesNotExistMessage);
            }

            _context.MasterEvents.Remove(masterEvent);
            _context.SaveChanges();
            this._loggerService.LogInformation(user, nameof(EventService), DeleteEventAction, masterEvent.Id);
        }
    }
}