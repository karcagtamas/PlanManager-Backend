using System;
using System.Threading.Tasks;
using EventManager.Client.Models.Events;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Events
{
    public class EventCreatorBase : ComponentBase
    {
        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        protected IEventService EventService { get; set; }
        
        [Inject]
        private IMatToaster Toaster { get; set; }
        protected EventModel Model { get; set; }

        protected override void OnInitialized()
        {
            this.Model = new EventModel
            {
                Title = "",
                Description = ""
            };
            base.OnInitialized();
        }

        protected async Task Create()
        {
            if (await this.EventService.CreateEvent(Model))
            {
                this.Model = new EventModel
                {
                    Title = "",
                    Description = ""
                };
            }
        }
    }
}