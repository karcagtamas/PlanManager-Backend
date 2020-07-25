using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EventManager.Client.Shared.Components.Friends {
    public class FriendRequestBase : ComponentBase {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        IFriendService FriendService { get; set; }

        [Inject]
        IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public FriendRequestModel Model { get; set; }
        public EditContext Context { get; set; }

        protected override void OnInitialized () {
            this.FormId = Parameters.Get<int>("FormId");

            ((ModalService)ModalService).OnConfirm += OnConfirm;

            this.Model = new FriendRequestModel {
                DestinationUserName = "",
                Message = ""
            };

            this.Context = new EditContext(this.Model);
        }

        protected async void OnConfirm()
        {
            var isValid = this.Context.Validate();
            if (isValid && await FriendService.SendFriendRequest(Model))
            {
                ModalService.Close(ModalResult.Ok<bool>(true));
                ((ModalService)ModalService).OnConfirm -= OnConfirm;
            }
        }
    }
}