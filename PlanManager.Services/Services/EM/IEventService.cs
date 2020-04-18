using System.Collections.Generic;
using PlanManager.Services.DTOs.EM;

namespace PlanManager.Services.Services.EM
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
        void SetEventLockedStatus(int eventId, bool status);
        void SetEventPublicStatus(int eventId, bool status);
        void SetEventDisabledStatus(int eventId, bool status);
    }
}