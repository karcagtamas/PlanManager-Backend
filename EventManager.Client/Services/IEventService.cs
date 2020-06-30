using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Events;

namespace EventManager.Client.Services {
    public interface IEventService {
        Task<ApiResponseModel<List<MyEventListDto>>> GetMyList ();
        Task<ApiResponseModel<EventDto>> Get (int id);
        Task<ApiResponseModel<Object>> CreateEvent (EventModel model);
        Task<ApiResponseModel<Object>> SetEventAsGt (int id);
        Task<ApiResponseModel<Object>> SetEventAsSport (int id);
        Task<ApiResponseModel<Object>> UpdateMasterEvent (MasterEventUpdateDto masterUpdate);
        Task<ApiResponseModel<Object>> UpdateSportEvent (SportEventUpdateDto sportUpdate);
        Task<ApiResponseModel<Object>> UpdateGtEvent (GtEventUpdateDto gtUpdate);
    }
}