using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models.EM;
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
        public MasterEventModel MasterUpdate { get; set; }
        public SportEventModel SportUpdate { get; set; }
        public GtEventModel GtUpdate { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.MasterUpdate = new MasterEventModel();
            this.SportUpdate = new SportEventModel();
            this.GtUpdate = new GtEventModel();
            await this.GetEvent();
            await base.OnInitializedAsync();
        }

        private async Task GetEvent()
        {
            this.Event = await this.EventService.Get(Id);
            this.MasterUpdate = new MasterEventModel(Event.MasterEvent);
            this.SportUpdate = this.Event.SportEvent != null
                ? new SportEventModel(this.Event.SportEvent)
                : new SportEventModel();
            this.GtUpdate = this.Event.GtEvent != null
                ? new GtEventModel(this.Event.GtEvent)
                : new GtEventModel();
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