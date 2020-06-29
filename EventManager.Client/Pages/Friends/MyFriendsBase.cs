using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.Friends;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Friends {
    public class MyFriendsBase : ComponentBase {
        [Inject]
        private IFriendService FriendService { get; set; }

        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }

        protected List<FriendListDto> Friends { get; set; } = null;
        protected List<FriendRequestListDto> FriendRequests { get; set; } = null;
        protected bool FriendRequestDialogIsOpen { get; set; } = false;
        protected bool MyFriendsIsLoading { get;set; } = false;
        protected bool MyFriendRequestsIsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync () {
            await GetFriends ();
            await GetFriendRequests ();
        }

        protected async Task GetFriendRequests () {
            this.MyFriendRequestsIsLoading = true;
            this.FriendRequests = await FriendService.GetMyFriendRequests ();
            this.MyFriendRequestsIsLoading = false;
        }

        protected async Task GetFriends () {
            this.MyFriendsIsLoading = true;
            this.Friends = await FriendService.GetMyFriends ();
            this.MyFriendsIsLoading = false;
        }

        protected async Task SendFriendRequestResponse (int id, bool response) {
            if (await this.FriendService.SendFriendRequestResponse (new FriendRequestResponseModel {
                    RequestId = id,
                        Response = response
                })) {
                await GetFriendRequests ();
            }
        }

        protected void OpenFriendRequestDialog () {
            this.FriendRequestDialogIsOpen = true;
        }

        protected void HandleFriendRequestDialogResponse (bool needRefresh) {
            this.FriendRequestDialogIsOpen = false;
        }
    }
}