using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models.EM;
using System.Collections.Generic;

namespace EventManager.Services.Services
{
    public interface IEventService
    {
        List<MyEventListDto> GetMyEvents();
        EventDto GetEvent(int eventId);
        void CreateEvent(EventModel model);
        void SetEventAsGtEvent(int eventId);
        void SetEventAsSportEvent(int eventId);
        void UpdateMasterEvent(MasterEventUpdateDto model);
        void UpdateSportEvent(SportEventUpdateDto model);
        void UpdateGtEvent(GtEventUpdateDto model);
        void DeleteEvent(int eventId);
    }
}