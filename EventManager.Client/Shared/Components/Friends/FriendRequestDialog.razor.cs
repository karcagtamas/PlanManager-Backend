using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EventManager.Client.Shared.Components.Friends
{
    public partial class FriendRequestDialog
    {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        private IFriendService FriendService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public FriendRequestModel Model { get; set; }
        public EditContext Context { get; set; }

        protected override void OnInitialized()
        {
            this.FormId = this.Parameters.Get<int>("FormId");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new FriendRequestModel
            {
                DestinationUserName = "",
                Message = ""
            };

            this.Context = new EditContext(this.Model);
        }

        protected async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            if (isValid && await this.FriendService.SendFriendRequest(this.Model))
            {
                this.ModalService.Close(ModalResult.Ok<bool>(true));
                ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
            }
        }
    }
}