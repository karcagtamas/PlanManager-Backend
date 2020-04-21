using System.Collections.Generic;
using System.Threading.Tasks;
using PlanManager.DataAccess.Entities;
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
        void SetEventLockedStatus(int eventId, bool status, User user);
        void SetEventPublicStatus(int eventId, bool status, User user);
        void SetEventDisabledStatus(int eventId, bool status, User user);
        void SetEventStatus(int eventId, string type, bool status);
    }
}