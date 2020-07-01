using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.User;
using EventManager.Client.Services.Interfaces;
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
            User = await UserService.GetUser();
            UserUpdate = new UserUpdateDto(User);
            Roles = string.Join(", ", User.Roles);
            if (User.ProfileImageData.Length != 0)
            {
                var base64 = Convert.ToBase64String(User.ProfileImageData);
                this.Image = $"data:image/gif;base64,{base64}";
            }
        }
        
        protected async Task GetGenders()
        {
            Genders = await UserService.GetGenders();
        }

        protected async Task UpdateUser()
        {
            if (await UserService.UpdateUser(UserUpdate)) {
                await GetUser();
            }
        }

        protected void DisableUser()
        {
            ShowConfirmDialog = true;
        }

        protected async Task HandleConfirmResponse(bool response)
        {
            ShowConfirmDialog = false;
            if (response && await UserService.DisableUser())
            {
                await AuthService.Logout();   
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