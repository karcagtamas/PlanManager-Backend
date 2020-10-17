using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Common
{
    public partial class CurrentUser
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
            this.User = null;
            await this.GetUser();
            await this.GetCountOfUnreadNotifications();
        }

        protected async Task GetUser()
        {
            this.User = await this.UserService.GetShortUser();
        }

        protected async Task GetCountOfUnreadNotifications()
        {
            int? val = await this.NotificationService.GetCountOfUnReadNotifications();
            this.UnReadNotificationCount = val == null ? 0 : (int)val;
        }

        protected async Task Logout()
        {
            await this.AuthService.Logout();
        }
    }
}