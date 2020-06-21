using EventManager.Client.Models.Notifications;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.Notifications
{
    public class MyNotificationsBase : ComponentBase
    {
        [Inject]
        public INotificationService NotificationService { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        public List<NotificationDto> Notifications { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetNotifications();
        }

        protected async Task GetNotifications()
        {
            try
            {
                var result = await NotificationService.GetMyNotifications();
                if (result.IsSuccess)
                {
                    Notifications = result.Content;
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "My Profile Error");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 
    }
}
