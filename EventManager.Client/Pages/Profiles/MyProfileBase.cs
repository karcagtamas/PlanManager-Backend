using System;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Profiles
{
    public class MyProfileBase : ComponentBase
    {
        [Inject] 
        private IUserService UserService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }
        
        [Inject]
        public IHelperService HelperService { get; set; }
        public UserDto User { get; set; }
        public UserUpdateDto UserUpdate { get; set; }
        
        public string Roles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetUser();
        }

        protected async Task GetUser()
        {
            try
            {
                var result = await UserService.GetUser();
                if (result.IsSuccess)
                {
                    User = result.Content;
                    UserUpdate = new UserUpdateDto(User);
                    Roles = string.Join(", ", Roles);
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "My Profile Error");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected async Task UpdateUser()
        {
            try
            {
                var result = await UserService.UpdateUser(UserUpdate);
                if (result.IsSuccess)
                {
                    await GetUser();
                    Toaster.Add("Successfully updated User", MatToastType.Success, "My Profile");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Master Event Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "My Profile Error");
                Console.WriteLine(e);
            }
        }
    }
}