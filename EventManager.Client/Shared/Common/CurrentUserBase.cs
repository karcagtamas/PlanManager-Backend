using System;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Common
{
    public class CurrentUserBase : ComponentBase
    {
        [Inject] 
        protected IUserService UserService { get; set; }
        
        [Inject]
        protected IHelperService HelperService { get; set; }
        
        [Inject]
        protected IAuthService AuthService { get; set; }

        [Inject]
        protected INotificationService NotificationService { get; set; }
        
        protected UserShortDto User { get; set; }
        protected int UnReadNotificationCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = null;
            await GetUser();
            await GetCountOfUnreadNotifications();
        }

        protected async Task GetUser()
        {
            User = await UserService.GetShortUser();
        }

        protected async Task GetCountOfUnreadNotifications()
        {
            int? val = await NotificationService.GetCountOfUnReadNotifications();
            UnReadNotificationCount = val == null ? 0 : (int)val;
        }

        protected async Task Logout()
        {
            await AuthService.Logout();
        }
    }
}