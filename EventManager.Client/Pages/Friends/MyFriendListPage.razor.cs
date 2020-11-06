using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.Friends;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.Friends
{
    public partial class MyFriendListPage
    {
        [Inject]
        private IFriendService FriendService { get; set; }

        [Inject]
        public IModalService Modal { get; set; }

        protected List<FriendListDto> Friends { get; set; } = null;
        protected List<FriendRequestListDto> FriendRequests { get; set; } = null;
        protected bool MyFriendsIsLoading { get; set; } = false;
        protected bool MyFriendRequestsIsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this.GetFriends();
            await this.GetFriendRequests();
        }

        protected async Task GetFriendRequests()
        {
            this.MyFriendRequestsIsLoading = true;
            this.StateHasChanged();
            this.FriendRequests = await this.FriendService.GetMyFriendRequests();
            this.StateHasChanged();
            this.MyFriendRequestsIsLoading = false;
            this.StateHasChanged();
        }

        protected async Task GetFriends()
        {
            this.MyFriendsIsLoading = true;
            this.StateHasChanged();
            this.Friends = await this.FriendService.GetMyFriends();
            this.StateHasChanged();
            this.MyFriendsIsLoading = false;
            this.StateHasChanged();
        }

        protected async Task SendFriendRequestResponse(int id, bool response)
        {
            if (await this.FriendService.SendFriendRequestResponse(new FriendRequestResponseModel
            {
                RequestId = id,
                Response = response
            }))
            {
                await this.GetFriendRequests();
            }
        }

        protected void OpenFriendRequestDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Save));

            this.Modal.OnClose += this.FriendRequestDialogClosed;

            this.Modal.Show<FriendRequestDialog>("Friend request", parameters, options);
        }

        protected async void FriendRequestDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetFriendRequests();
            }

            this.Modal.OnClose -= this.FriendRequestDialogClosed;
        }

        protected void OpenFriendDataModal(string friendId)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 2);
            parameters.Add("friend", friendId);

            var options = new ModalOptions();
            options.ButtonOptions.CancelButtonType = CancelButton.Close;

            this.Modal.OnClose += this.FriendDataModalClosed;

            this.Modal.Show<FriendData>("Friend data form", parameters, options);
        }

        protected async void FriendDataModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetFriends();
            }

            this.Modal.OnClose -= this.FriendDataModalClosed;
        }
    }
}