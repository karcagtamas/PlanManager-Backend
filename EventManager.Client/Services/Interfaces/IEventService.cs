using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Events;

namespace EventManager.Client.Services.Interfaces {
    public interface IEventService {
        Task<List<MyEventListDto>> GetMyList ();
        Task<EventDto> Get (int id);
        Task<bool> CreateEvent (EventModel model);
        Task<bool> SetEventAsGt (int id);
        Task<bool> SetEventAsSport (int id);
        Task<bool> UpdateMasterEvent (MasterEventUpdateDto masterUpdate);
        Task<bool> UpdateSportEvent (SportEventUpdateDto sportUpdate);
        Task<bool> UpdateGtEvent (GtEventUpdateDto gtUpdate);
    }
}