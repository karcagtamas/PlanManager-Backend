using EventManager.Client.Models;
using EventManager.Client.Models.Tasks;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.Tasks
{
    public class TaskDialogBase : ComponentBase
    {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        ITaskService TaskService { get; set; }

        [Inject]
        IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public TaskModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; } = false;
        public int Id { get; set; }
        public TaskDataDto Task { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");
            this.Id = Parameters.TryGet<int>("task");


            ((ModalService)ModalService).OnConfirm += OnConfirm;

            this.Model = new TaskModel
            {
                Title = "",
                Description = "",
                Deadline = DateTime.Now
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Task = await TaskService.GetTask(this.Id);
                this.Model = new TaskModel
                {
                    Title = this.Task.Title,
                    Description = this.Task.Description,
                    Deadline = this.Task.Deadline
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
                var task = new TaskDataDto
                {
                    Id = this.Id,
                    Title = this.Model.Title,
                    Description = this.Model.Description,
                    Deadline = this.Model.Deadline.ToLocalTime(),
                    IsSolved = this.Task.IsSolved
                };
                if (isValid && await TaskService.UpdateTask(task))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await TaskService.CreateTask(this.Model))
                {
                    ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}
