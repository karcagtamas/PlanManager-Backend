using System.Collections.Generic;
using ManagerAPI.Models.DTOs;

namespace EventManager.Services.Services
{
    public interface IEventService
    {
        List<MyEventListDto> GetMyEvents();
        EventDto GetEvent(int eventId);
        void CreateEvent(EventCreateDto model);
        void SetEventAsGtEvent(int eventId);
        void SetEventAsSportEvent(int eventId);
        void UpdateMasterEvent(MasterEventUpdateDto model);
        void UpdateSportEvent(SportEventUpdateDto model);
        void UpdateGtEvent(GtEventUpdateDto model);
        void DeleteEvent(int eventId);
    }
}