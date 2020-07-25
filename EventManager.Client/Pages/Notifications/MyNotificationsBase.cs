using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
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
        private INotificationService NotificationService { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        private List<NotificationDto> Notifications { get; set; }
        protected List<NotificationDto> FilteredNotifications { get; set; }
        private bool ShowRead { get; set; } = false;
        private int? Importance { get; set; } = null;
        protected bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await GetNotifications();
            await SetUnReadsToRead();
        }

        private async Task GetNotifications()
        {
            this.IsLoading = true;
            Notifications = await NotificationService.GetMyNotifications();
            if (Notifications.Any()) {
                FilterNotifications();
            }
            this.IsLoading = false;
        }

        private async Task SetUnReadsToRead()
        {
            try
            {
                var result = await NotificationService.SetUnReadsToRead((from i in Notifications where !i.IsRead select i.Id).ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void ShowReadValueChangedEvent(bool value)
        {
            this.IsLoading = true;
            ShowRead = value;            
            FilterNotifications();
            this.IsLoading = false;
            StateHasChanged();
        }

        protected void ImportanceValueChangedEvent(int? value)
        {
            this.IsLoading = true;
            Importance = value;
            FilterNotifications();
            this.IsLoading = false;
            StateHasChanged();
        }

        private void FilterNotifications()
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
