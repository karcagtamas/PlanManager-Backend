using System;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Events;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Events.Body
{
    public class EventDataBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IEventService EventService { get; set; }

        [Inject] public IHelperService HelperService { get; set; }

        [Inject] public IMatToaster Toaster { get; set; }

        public EventDto Event { get; set; }

        public MasterEventUpdateDto MasterUpdate { get; set; }
        public SportEventUpdateDto SportUpdate { get; set; }
        public GtEventUpdateDto GtUpdate { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MasterUpdate = new MasterEventUpdateDto();
            SportUpdate = new SportEventUpdateDto();
            GtUpdate = new GtEventUpdateDto();
            await GetEvent();
            await base.OnInitializedAsync();
        }

        private async Task GetEvent()
        {
            try
            {
                var result = await EventService.Get(Id);
                if (result.IsSuccess)
                {
                    Event = result.Content;
                    MasterUpdate = new MasterEventUpdateDto(Event.MasterEvent);
                    SportUpdate = Event.SportEvent != null
                        ? new SportEventUpdateDto(Event.SportEvent)
                        : new SportEventUpdateDto();
                    GtUpdate = Event.GtEvent != null ? new GtEventUpdateDto(Event.GtEvent) : new GtEventUpdateDto();
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Event Error");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected async Task SetEventAsGt()
        {
            try
            {
                var result = await EventService.SetEventAsGt(Id);
                if (result.IsSuccess)
                {
                    await GetEvent();
                    Toaster.Add("Successfully set event as GT", MatToastType.Success, "GT Event");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "GT Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "GT Event Error");
                Console.WriteLine(e);
            }
        }

        protected async Task SetEventAsSport()
        {
            try
            {
                var result = await EventService.SetEventAsSport(Id);
                if (result.IsSuccess)
                {
                    await GetEvent();
                    Toaster.Add("Successfully set event as Sport", MatToastType.Success, "Sport Event");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Sport Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "Sport Event Error");
                Console.WriteLine(e);
            }
        }

        protected async Task UpdateMaster()
        {
            try
            {
                var result = await EventService.UpdateMasterEvent(MasterUpdate);
                if (result.IsSuccess)
                {
                    await GetEvent();
                    Toaster.Add("Successfully updated Master Event", MatToastType.Success, "Master Event");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Master Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "Master Event Error");
                Console.WriteLine(e);
            }
        }

        protected async Task UpdateSport()
        {
            try
            {
                var result = await EventService.UpdateSportEvent(SportUpdate);
                if (result.IsSuccess)
                {
                    await GetEvent();
                    Toaster.Add("Successfully updated Sport Event", MatToastType.Success, "Sport Event");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Sport Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "Sport Event Error");
                Console.WriteLine(e);
            }
        }

        protected async Task UpdateGt()
        {
            try
            {
                var result = await EventService.UpdateGtEvent(GtUpdate);
                if (result.IsSuccess)
                {
                    Toaster.Add("Successfully updated GT Event", MatToastType.Success, "GT Event");
                    await GetEvent();
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "GT Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "GT Event Error");
                Console.WriteLine(e);
            }
        }
    }
}