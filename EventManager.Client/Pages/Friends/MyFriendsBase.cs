using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Friends;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.Friends;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Friends {
    public class MyFriendsBase : ComponentBase {
        [Inject]
        private IFriendService FriendService { get; set; }

        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        public IModalService Modal { get; set; }

        protected List<FriendListDto> Friends { get; set; } = null;
        protected List<FriendRequestListDto> FriendRequests { get; set; } = null;
        protected bool MyFriendsIsLoading { get; set; } = false;
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
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions();
            options.ShowCancelButton = true;
            options.ShowConfirmButton = true;
            options.CancelButtonType = CancelButton.Cancel;
            options.ConfirmButtonType = ConfirmButton.Save;

            Modal.Show<FriendRequest>("Friend request", parameters, options);
        }

        protected void ShowModal () {
            var parameters = new ModalParameters ();
            parameters.Add ("FormId", 2);

            var options = new ModalOptions();
            options.ShowConfirmButton = true;

            Modal.OnClose += ModalClosed;
            Modal.Show<FriendData> ("Friend data form", parameters, options);
        }

        protected void ModalClosed (ModalResult modalResult) {
            if (modalResult.Cancelled) {
                Console.WriteLine ("Modal cancelled");
            } else {
                Console.WriteLine (modalResult.Data.ToString ());
            }

            Modal.OnClose -= ModalClosed;
        }
    }
}