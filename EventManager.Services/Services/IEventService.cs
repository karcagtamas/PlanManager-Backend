using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models.EM;

namespace EventManager.Services.Services
{
    public interface IEventService
    {
        List<MyEventListDto> GetMyEvents();
        EventDto GetEvent(int eventId);
        void CreateEvent(EventModel model);
        void SetEventAsGtEvent(int eventId);
        void SetEventAsSportEvent(int eventId);
        void UpdateMasterEvent(MasterEventModel model);
        void UpdateSportEvent(SportEventModel model);
        void UpdateGtEvent(GtEventModel model);
        void DeleteEvent(int eventId);
    }
}