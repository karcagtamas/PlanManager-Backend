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
            await GetFriends();
            await GetFriendRequests();
        }

        protected async Task GetFriendRequests()
        {
            this.MyFriendRequestsIsLoading = true;
            StateHasChanged();
            this.FriendRequests = await FriendService.GetMyFriendRequests();
            StateHasChanged();
            this.MyFriendRequestsIsLoading = false;
            StateHasChanged();
        }

        protected async Task GetFriends()
        {
            this.MyFriendsIsLoading = true;
            StateHasChanged();
            this.Friends = await FriendService.GetMyFriends();
            StateHasChanged();
            this.MyFriendsIsLoading = false;
            StateHasChanged();
        }

        protected async Task SendFriendRequestResponse(int id, bool response)
        {
            if (await this.FriendService.SendFriendRequestResponse(new FriendRequestResponseModel
            {
                RequestId = id,
                Response = response
            }))
            {
                await GetFriendRequests();
            }
        }

        protected void OpenFriendRequestDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Save));

            Modal.OnClose += FriendRequestDialogClosed;

            Modal.Show<FriendRequestDialog>("Friend request", parameters, options);
        }

        protected async void FriendRequestDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetFriendRequests();
            }

            Modal.OnClose -= FriendRequestDialogClosed;
        }

        protected void OpenFriendDataModal(string friendId)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 2);
            parameters.Add("friend", friendId);

            var options = new ModalOptions();
            options.ButtonOptions.CancelButtonType = CancelButton.Close;

            Modal.OnClose += FriendDataModalClosed;

            Modal.Show<FriendData>("Friend data form", parameters, options);
        }

        protected async void FriendDataModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await GetFriends();
            }

            Modal.OnClose -= FriendDataModalClosed;
        }
    }
}