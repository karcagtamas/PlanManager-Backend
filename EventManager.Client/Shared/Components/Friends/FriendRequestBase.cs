using System.Threading.Tasks;
using EventManager.Client.Models.Friends;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.Friends {
    public class FriendRequestBase : ComponentBase {
        [Parameter]
        public bool DialogIsOpen { get; set; }

        [Parameter]
        public EventCallback<bool> Response { get; set; }

        [Inject]
        IFriendService FriendService { get; set; }

        public FriendRequestModel Model { get; set; }

        protected override void OnInitialized () {
            this.Model = new FriendRequestModel {
                DestinationUserName = "",
                Message = ""
            };
        }

        protected async Task Save () {
            if (await FriendService.SendFriendRequest (Model)) {
                await Response.InvokeAsync (true);
            }
        }

        protected async Task Cancel () {
            this.Model = new FriendRequestModel {
                DestinationUserName = "",
                Message = ""
            };
            await Response.InvokeAsync (false);
        }
    }
}