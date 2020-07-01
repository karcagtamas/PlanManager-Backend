using EventManager.Client.Models.Notifications;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<NotificationDto> FilteredNotifications { get; set; }
        protected bool ShowRead { get; set; } = false;
        protected int? Importance { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await GetNotifications();
            await SetUnReadsToRead();
        }

        protected async Task GetNotifications()
        {

            Notifications = await NotificationService.GetMyNotifications();
            if (Notifications.Count() > 0) {
                FilterNotifications();
            }
        }
        
        protected async Task SetUnReadsToRead()
        {
            List<int> ids = new List<int>();
            foreach (var i in Notifications)
            {
                if (!i.IsRead)
                {
                    ids.Add(i.Id);
                }
            }

            try
            {
                var result = await NotificationService.SetUnReadsToRead(ids.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void ShowReadValueChangedEvent(bool value)
        {
            ShowRead = value;            
            FilterNotifications();
        }

        protected void ImportanceValueChangedEvent(int? value)
        {
            Importance = value;
            FilterNotifications();
        }

        protected void FilterNotifications()
        {
            var query = Notifications.AsQueryable();
            if (!ShowRead)
            {
                query = query.Where(x => !x.IsRead);
            }
            if (Importance != null)
            {
                query = query.Where(x => x.ImportanceLevel == Importance);
            }
            FilteredNotifications = query.ToList();
        }
    }
}
