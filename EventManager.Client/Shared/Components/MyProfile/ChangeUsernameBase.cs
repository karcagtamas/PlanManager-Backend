using System;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public class ChangeUsernameBase : ComponentBase
    {
        [Parameter] public bool DialogIsOpen { get; set; }

        [Parameter] public EventCallback<bool> Response { get; set; }

        [Inject] private IUserService UserService { get; set; }

        [Inject] private IMatToaster Toaster { get; set; }

        [Inject] private IHelperService HelperService { get; set; }

        protected UsernameUpdateModel UsernameUpdate { get; set; }

        protected override void OnInitialized()
        {
            this.UsernameUpdate = new UsernameUpdateModel
            {
                UserName = ""
            };
        }

        protected async Task Save()
        {
            if (await UserService.UpdateUsername(this.UsernameUpdate)) {
                await Response.InvokeAsync(true);
            }
        }

        protected async Task Cancel()
        {
            this.UsernameUpdate = new UsernameUpdateModel
            {
                UserName = ""
            };
            await Response.InvokeAsync(false);
        }
    }
}