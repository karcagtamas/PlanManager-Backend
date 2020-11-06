using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.Tasks
{
    public partial class TaskDialog
    {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject]
        private ITaskService TaskService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public TaskModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; } = false;
        public int Id { get; set; }
        public TaskDto Task { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("task");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new TaskModel
            {
                Title = "",
                Description = "",
                Deadline = DateTime.Now
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Task = await this.TaskService.Get(this.Id);
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
            bool isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await this.TaskService.Update(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
            else
            {
                if (isValid && await this.TaskService.Create(this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok<bool>(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }
    }
}