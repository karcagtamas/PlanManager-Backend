using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using EventManager.Client.Shared.Components.MyProfile;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.Profiles
{
    public partial class MyProfilePage
    {
        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        public IHelperService HelperService { get; set; }

        [Inject]
        public IGenderService GenderService { get; set; }

        [Inject]
        public IModalService Modal { get; set; }

        public UserDto User { get; set; }
        public UserModel UserUpdate { get; set; }
        protected List<GenderListDto> Genders { get; set; }

        protected bool ShowConfirmDialog { get; set; } = false;
        protected bool ShowChangePasswordDialog { get; set; } = false;
        protected bool ShowUploadProfileImageDialog { get; set; } = false;
        protected bool ShowChangeUsernameDialog { get; set; } = false;
        protected string Image { get; set; }
        public string Roles { get; set; }
        protected bool ProfileIsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await GetUser();
            await GetGenders();
        }

        private async Task GetUser()
        {
            this.ProfileIsLoading = true;
            User = await UserService.GetUser();
            UserUpdate = new UserModel(User);
            Roles = string.Join(", ", User.Roles);
            if (User.ProfileImageData.Length != 0)
            {
                var base64 = Convert.ToBase64String(User.ProfileImageData);
                this.Image = $"data:image/gif;base64,{base64}";
            }
            this.ProfileIsLoading = false;
            StateHasChanged();
        }

        private async Task GetGenders()
        {
            Genders = await this.GenderService.GetAll("Name");
        }

        protected async Task UpdateUser()
        {
            if (await UserService.UpdateUser(UserUpdate))
            {
                await GetUser();
            }
        }

        protected void OpenUserDisableConfirmDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Disable);
            parameters.Add("name", "yourself");

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += DisableConfirmDialogClosed;
            Modal.Show<Confirm>("User disable", parameters, options);
        }

        protected async void DisableConfirmDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await UserService.DisableUser())
            {
                await AuthService.Logout();
            }
            Modal.OnClose -= DisableConfirmDialogClosed;
        }

        protected void OpenChangePasswordDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 2);

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Save));

            Modal.OnClose += ChangePasswordDialogClosed;
            Modal.Show<ChangePasswordDialog>("Change password", parameters, options);
        }

        protected async void ChangePasswordDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await AuthService.Logout();
            }
            Modal.OnClose -= ChangePasswordDialogClosed;
        }

        protected void OpenUploadProfileImageDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 3);

            var options = new ModalOptions(new ModalButtonOptions(true, false, CancelButton.Cancel, ConfirmButton.Save));

            Modal.OnClose += UploadProfileImageDialogClosed;
            Modal.Show<UploadProfileImageDialog>("Profile image upload", parameters, options);
        }

        protected async void UploadProfileImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                Console.WriteLine(modalResult.Data);
                await GetUser();
            }
            Modal.OnClose -= UploadProfileImageDialogClosed;
        }

        protected void OpenChangeUsernameDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 4);

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Save));

            Modal.OnClose += ChangeUsernameDialogClosed;
            Modal.Show<ChangeUsernameDialog>("Change user name", parameters, options);
        }

        protected async void ChangeUsernameDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await AuthService.Logout();
            }
            Modal.OnClose -= ChangeUsernameDialogClosed;
        }
    }
}