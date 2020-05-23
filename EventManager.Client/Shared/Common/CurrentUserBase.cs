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
        
        protected UserShortDto User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = null;
            await GetUser();
        }

        protected async Task GetUser()
        {
            try
            {
                var result = await UserService.GetShortUser();
                User = result.IsSuccess ? result.Content : null;
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