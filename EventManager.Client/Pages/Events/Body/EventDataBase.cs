using System;
using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.EM;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Events.Body
{
    public class EventDataBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IEventService EventService { get; set; }

        [Inject]
        public IHelperService HelperService { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        public EventDto Event { get; set; }
        public MasterEventUpdateDto MasterUpdate { get; set; }
        public SportEventUpdateDto SportUpdate { get; set; }
        public GtEventUpdateDto GtUpdate { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.MasterUpdate = new MasterEventUpdateDto();
            this.SportUpdate = new SportEventUpdateDto();
            this.GtUpdate = new GtEventUpdateDto();
            await this.GetEvent();
            await base.OnInitializedAsync();
        }

        private async Task GetEvent()
        {
            this.Event = await this.EventService.Get(Id);
            this.MasterUpdate = new MasterEventUpdateDto(Event.MasterEvent);
            this.SportUpdate = this.Event.SportEvent != null
                ? new SportEventUpdateDto(this.Event.SportEvent)
                : new SportEventUpdateDto();
            this.GtUpdate = this.Event.GtEvent != null
                ? new GtEventUpdateDto(this.Event.GtEvent)
                : new GtEventUpdateDto();
        }

        protected async Task SetEventAsGt()
        {
            if (await this.EventService.SetEventAsGt(this.Id))
            {
                await this.GetEvent();
            }
        }

        protected async Task SetEventAsSport()
        {
            if (await this.EventService.SetEventAsSport(this.Id))
            {
                await this.GetEvent();
            }
        }

        protected async Task UpdateMaster()
        {
            if (await this.EventService.UpdateMasterEvent(this.MasterUpdate))
            {
                await this.GetEvent();
            }
        }

        protected async Task UpdateSport()
        {
            if (await this.EventService.UpdateSportEvent(this.SportUpdate))
            {
                await this.GetEvent();
            }
        }

        protected async Task UpdateGt()
        {
            if (await this.EventService.UpdateGtEvent(this.GtUpdate))
            {
                await this.GetEvent();
            }
        }
    }
}