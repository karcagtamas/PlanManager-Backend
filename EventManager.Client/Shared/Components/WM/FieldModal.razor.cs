using System.Threading.Tasks;
using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EventManager.Client.Shared.Components.WM
{
    public partial class FieldModal
    {
        [CascadingParameter] private ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] IWorkingFieldService WorkingFieldService { get; set; }

        [Inject] IModalService ModalService { get; set; }

        private int FormId { get; set; }
        private WorkingFieldModel Model { get; set; }
        private EditContext Context { get; set; }
        private bool IsEdit { get; set; } = false;
        private int Id { get; set; }
        private int WorkingDayId { get; set; }
        private WorkingFieldDto Field { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");
            this.Id = Parameters.TryGet<int>("field");
            this.WorkingDayId = Parameters.Get<int>("working-day");


            ((ModalService) ModalService).OnConfirm += OnConfirm;

            this.Model = new WorkingFieldModel
            {
                Title = "",
                Description = "",
                Length = 1
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Field = await WorkingFieldService.Get(this.Id);
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
            var isValid = this.Context.Validate();
            this.Model.WorkingDayId = this.WorkingDayId;
            if (this.IsEdit)
            {
                if (isValid && await WorkingFieldService.Update(this.Id, this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await WorkingFieldService.Create(this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
        }

        private async void DeleteField()
        {
            if (await this.WorkingFieldService.Delete(this.Id))
            {
                ModalService.Close(ModalResult.Ok<bool>(true));
            }
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 2);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Field.Title);

            ((ModalService) ModalService).OnConfirm -= OnConfirm;

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            ModalService.OnClose += DeleteDialogClosed;
            ModalService.Show<Confirm>("Working Field Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data && await this.WorkingFieldService.Delete(this.Id))
            {
                ModalService.Close(ModalResult.Ok<bool>(true));
            }

            ModalService.OnClose -= DeleteDialogClosed;
        }
    }
}