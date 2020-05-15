using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Events.Body
{
    public class EventNotificationsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
    }
}