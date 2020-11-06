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
    public partial class MyNotificationListPage
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
            await this.GetNotifications();
            await this.SetUnReadsToRead();
        }

        private async Task GetNotifications()
        {
            this.IsLoading = true;
            this.Notifications = await this.NotificationService.GetMyNotifications();
            if (this.Notifications.Any())
            {
                this.FilterNotifications();
            }
            this.IsLoading = false;
        }

        private async Task SetUnReadsToRead()
        {
            try
            {
                bool result = await this.NotificationService.SetUnReadsToRead((from i in this.Notifications where !i.IsRead select i.Id).ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void ShowReadValueChangedEvent(bool value)
        {
            this.IsLoading = true;
            this.ShowRead = value;
            this.FilterNotifications();
            this.IsLoading = false;
            this.StateHasChanged();
        }

        protected void ImportanceValueChangedEvent(int? value)
        {
            this.IsLoading = true;
            this.Importance = value;
            this.FilterNotifications();
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void FilterNotifications()
        {
            var query = this.Notifications.AsQueryable();
            if (!this.ShowRead)
            {
                query = query.Where(x => !x.IsRead);
            }
            if (this.Importance != null)
            {
                query = query.Where(x => x.ImportanceLevel == this.Importance);
            }
            this.FilteredNotifications = query.ToList();
        }
    }
}