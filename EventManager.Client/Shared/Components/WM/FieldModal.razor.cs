using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.WM
{
    public partial class FieldModal
    {
        [CascadingParameter] private ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private IWorkingFieldService WorkingFieldService { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        private int FormId { get; set; }
        private WorkingFieldModel Model { get; set; }
        private EditContext Context { get; set; }
        private bool IsEdit { get; set; } = false;
        private int Id { get; set; }
        private int WorkingDayId { get; set; }
        private WorkingFieldDto Field { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("field");
            this.WorkingDayId = this.Parameters.Get<int>("working-day");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new WorkingFieldModel
            {
                Title = "",
                Description = "",
                Length = 1
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Field = await this.WorkingFieldService.Get(this.Id);
                this.Model = new WorkingFieldModel
                {
                    Title = this.Field.Title,
                    Description = this.Field.Description,
                    Length = this.Field.Length
                };
                this.IsEdit = true;
                this.Context = new EditContext(this.Model);
            }
        }

        private async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            this.Model.WorkingDayId = this.WorkingDayId;
            if (this.IsEdit)
            {
                if (isValid && await this.WorkingFieldService.Update(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
            else
            {
                if (isValid && await this.WorkingFieldService.Create(this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }

        private async void DeleteField()
        {
            if (await this.WorkingFieldService.Delete(this.Id))
            {
                this.ModalService.Close(ModalResult.Ok<bool>(true));
            }
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 2);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Field.Title);

            ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            this.ModalService.OnClose += this.DeleteDialogClosed;
            this.ModalService.Show<Confirm>("Working Field Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.WorkingFieldService.Delete(this.Id))
            {
                this.ModalService.Close(ModalResult.Ok<bool>(true));
            }

            this.ModalService.OnClose -= this.DeleteDialogClosed;
        }
    }
}