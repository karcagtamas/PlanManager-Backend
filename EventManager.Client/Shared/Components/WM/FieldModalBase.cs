using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EventManager.Client.Shared.Components.WM
{
    public class FieldModalBase : ComponentBase
    {
        [CascadingParameter] 
        private ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        IWorkingFieldService WorkingFieldService { get; set; }

        [Inject]
        IModalService ModalService { get; set; }

        private int FormId { get; set; }
        protected WorkingFieldModel Model { get; set; }
        protected EditContext Context { get; set; }
        private bool IsEdit { get; set; } = false;
        protected int Id { get; set; }
        private int WorkingDayId { get; set; }
        private WorkingFieldDto Field { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");
            this.Id = Parameters.TryGet<int>("field");
            this.WorkingDayId = Parameters.Get<int>("working-day");


            ((ModalService)ModalService).OnConfirm += OnConfirm;

            this.Model = new WorkingFieldModel
            {
                Title = "",
                Description = "",
                Length = 1
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Field = await WorkingFieldService.GetWorkingField(this.Id);
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
                if (isValid && await WorkingFieldService.UpdateWorkingField(this.Id, this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await WorkingFieldService.AddWorkingField(this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
        }

        protected async void DeleteField() {
            if (await this.WorkingFieldService.DeleteWorkingField(this.Id)) {
                ModalService.Close(ModalResult.Ok<bool>(true));
            }
        }
    }
}