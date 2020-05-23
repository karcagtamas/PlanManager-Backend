using System;
using System.Collections.Generic;
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
        private IAuthService AuthService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }
        
        [Inject]
        public IHelperService HelperService { get; set; }
        public UserDto User { get; set; }
        public UserUpdateDto UserUpdate { get; set; }
        protected List<GenderDto> Genders { get; set; }

        protected bool ShowConfirmDialog { get; set; } = false;
        protected bool ShowChangePasswordDialog { get; set; } = false;
        protected bool ShowUploadProfileImageDialog { get; set; } = false;
        protected bool ShowChangeUsernameDialog { get; set; } = false;
        protected string Image { get; set; }
        
        public string Roles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetUser();
            await GetGenders();
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
                    if (User.ProfileImageData.Length != 0)
                    {
                        var base64 = Convert.ToBase64String(User.ProfileImageData);
                        this.Image = $"data:image/gif;base64,{base64}";
                    }
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
        
        protected async Task GetGenders()
        {
            try
            {
                var result = await UserService.GetGenders();
                if (result.IsSuccess)
                {
                    Genders = result.Content;
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
                    Toaster.Add(result.Message, MatToastType.Danger, "My Profile Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "My Profile Error");
                Console.WriteLine(e);
            }
        }

        protected void DisableUser()
        {
            ShowConfirmDialog = true;
        }

        protected async Task HandleConfirmResponse(bool response)
        {
            ShowConfirmDialog = false;
            if (response)
            {
                try
                {
                    var result = await UserService.DisableUser();
                    if (result.IsSuccess)
                    {
                        Toaster.Add("Successfully disabled User", MatToastType.Success, "My Profile");
                        await AuthService.Logout();
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
        }

        protected void OpenChangePasswordDialog()
        {
            ShowChangePasswordDialog = true;
        }

        protected async Task HandleChangePasswordResponse(bool needLogout)
        {
            ShowChangePasswordDialog = false;
            if (needLogout)
            {
                await AuthService.Logout();
            }
        }

        protected void OpenUploadProfileImageDialog()
        {
            ShowUploadProfileImageDialog = true;
        }

        protected async Task HandleUploadProfileImageResponse(bool needRefresh)
        {
            if (needRefresh)
            {
                await GetUser();
            }
            ShowUploadProfileImageDialog = false;
        }
        
        protected void OpenChangeUsernameDialog()
        {
            ShowChangeUsernameDialog = true;
        }

        protected async Task HandleChangeUsernameResponse(bool needLogout)
        {    
            if (needLogout)
            {
                await AuthService.Logout();
            }
            ShowChangeUsernameDialog = false;
        }
    }
}