using System;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
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
            try
            {
                var result = await UserService.UpdateUsername(this.UsernameUpdate);
                if (result.IsSuccess)
                {
                    Toaster.Add("Successfully updated username", MatToastType.Success, "My Profile");
                    await Response.InvokeAsync(true);
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "My Profile Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "My Profile Error");
                Console.WriteLine(e);
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