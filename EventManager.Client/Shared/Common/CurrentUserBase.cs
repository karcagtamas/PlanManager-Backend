using System;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services;
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
            try
            {
                var result = await NotificationService.GetCountOfUnReadNotifications();
                UnReadNotificationCount = result.IsSuccess ? result.Content : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected async Task Logout()
        {
            await AuthService.Logout();
        }
    }
}