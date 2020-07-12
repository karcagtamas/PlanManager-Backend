using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.Events;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Events
{
    public class MyEventsBase : ComponentBase
    {
        [Inject]
        public IEventService EventService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }

        [Inject]
        public IHelperService HelperService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<MyEventListDto> MyEvents { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetMyListEventList();
            await base.OnInitializedAsync();
        }

        private async Task GetMyListEventList()
        {
            this.MyEvents = await EventService.GetMyList();
        }

        protected void RedirectTo(int id)
        {
            NavigationManager.NavigateTo($"/events/{id}/data");
        }
    }
}