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
            try
            {
                var result = await EventService.CreateEvent(Model);
                if (result.IsSuccess)
                {
                    Toaster.Add("Creation was successful", MatToastType.Success, "Event Create Successful");
                    this.Model = new EventModel
                    {
                        Title = "",
                        Description = ""
                    };
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Event Create Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "Event Create Error");
                Console.WriteLine(e);
            }
        }
    }
}