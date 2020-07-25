using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public class ChangeUsernameBase : ComponentBase
    {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject] 
        private IUserService UserService { get; set; }

        [Inject]
        IModalService ModalService { get; set; }
        public int FormId { get; set; }

        protected UsernameUpdateModel UsernameUpdate { get; set; }

        public EditContext Context { get; set; }

        protected override void OnInitialized()
        {
            this.FormId = Parameters.Get<int>("FormId");

            ((ModalService)ModalService).OnConfirm += OnConfirm;

            this.UsernameUpdate = new UsernameUpdateModel
            {
                UserName = ""
            };

            this.Context = new EditContext(this.UsernameUpdate);
        }

        protected async void OnConfirm()
        {
            var isValid = this.Context.Validate();
            if (isValid && await UserService.UpdateUsername(this.UsernameUpdate))
            {
                ModalService.Close(ModalResult.Ok<bool>(true));
                ((ModalService)ModalService).OnConfirm -= OnConfirm;
            }
        }
    }
}