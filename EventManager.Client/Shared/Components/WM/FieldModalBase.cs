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
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        IWorkingManagerService WorkingManagerService { get; set; }

        [Inject]
        IModalService ModalService { get; set; }

        public int FormId { get; set; }
        public WorkingFieldModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; } = false;
        public int Id { get; set; }
        public int WorkingDayId { get; set; }
        public WorkingFieldDto Field { get; set; }

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
                this.Field = await WorkingManagerService.GetWorkingField(this.Id);
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

        protected async void OnConfirm()
        {
            var isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await WorkingManagerService.UpdateWorkingField(this.Id, this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await WorkingManagerService.AddWorkingField(this.WorkingDayId, this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
        }

        protected async void DeleteField() {
            if (await this.WorkingManagerService.DeleteWorkingField(this.Id)) {
                ModalService.Close(ModalResult.Ok<bool>(true));
            }
        }
    }
}