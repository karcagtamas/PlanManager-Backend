using EventManager.Client.Models;
using EventManager.Client.Models.Tasks;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.Tasks;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.MyTasks
{
    public class MyTasksBase : ComponentBase
    {
        [Inject]
        private ITaskService TaskService { get; set; }

        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        public IModalService Modal { get; set; }

        protected List<TaskDateDto> TaskList { get; set; }
        protected bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this.GetTasks();
        }

        protected async Task GetTasks()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.TaskList = await this.TaskService.GetTasks(null);
            this.IsLoading = false;
            StateHasChanged();
        }

        protected async void IsSolvedChanged(bool newValue, int taskId, TaskDateDto group) 
        {
            var task = this.TaskList.SelectMany(x => x.TaskList).Where(x => x.Id == taskId).FirstOrDefault();

            var taskData = await TaskService.GetTask(taskId);
            if (taskData != null)
            {
                taskData.IsSolved = newValue;
                task.IsSolved = await TaskService.UpdateTask(taskData) ? newValue : !newValue;
                group.AllSolved = group.TaskList.Where(x => !x.IsSolved).Count() == 0;
                group.OutOfRange = group.Deadline < DateTime.Now && !group.AllSolved;

                StateHasChanged();
            }
        }

        protected void OpenAddTaskModal()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions();
            options.ButtonOptions.ConfirmButtonType = ConfirmButton.Save;
            options.ButtonOptions.ShowConfirmButton = true;

            Modal.OnClose += TaskModalClosed;

            Modal.Show<TaskDialog>("Create Task", parameters, options);
        }

        protected void OpenUpdateTaskModal(int taskId)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("task", taskId);

            var options = new ModalOptions();
            options.ButtonOptions.ConfirmButtonType = ConfirmButton.Save;
            options.ButtonOptions.ShowConfirmButton = true;

            Modal.OnClose += TaskModalClosed;

            Modal.Show<TaskDialog>("Update Task", parameters, options);
        }

        protected async void TaskModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetTasks();
            }

            Modal.OnClose -= TaskModalClosed;
        }
    }
}
