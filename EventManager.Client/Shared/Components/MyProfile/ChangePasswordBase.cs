using System;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public class ChangePasswordBase : ComponentBase
    {
        [Parameter]
        public bool DialogIsOpen { get; set; }
        
        [Parameter]
        public EventCallback<bool> Response { get; set; }
        
        [Inject]
        private IUserService UserService { get; set; }
        
        [Inject]
        private IMatToaster Toaster { get; set; }
        
        [Inject]
        private IHelperService HelperService { get; set; }
        
        protected PasswordUpdateModel PasswordUpdate { get; set; }

        protected override void OnInitialized()
        {
            this.PasswordUpdate = new PasswordUpdateModel
            {
                NewPassword = "",
                OldPassword = "",
                ConfirmNewPassword = ""
            };
        }

        protected async Task Save()
        {
            try
            {
                var result = await UserService.UpdatePassword("");
                if (result.IsSuccess)
                {
                    Toaster.Add("Successfully updated password", MatToastType.Success, "My Profile");
                    // DialogIsOpen = false;
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
            this.PasswordUpdate = new PasswordUpdateModel
            {
                OldPassword = "",
                NewPassword = "",
                ConfirmNewPassword = ""
            };
            await Response.InvokeAsync(false);
        }
    }
}