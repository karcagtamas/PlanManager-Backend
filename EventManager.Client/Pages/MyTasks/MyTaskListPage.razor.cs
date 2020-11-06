using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using EventManager.Client.Shared.Components.Tasks;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.MyTasks
{
    public partial class MyTaskListPage
    {
        [Inject] private ITaskService TaskService { get; set; }

        [Inject] private IModalService Modal { get; set; }

        protected List<TaskDateDto> TaskList { get; set; }
        protected bool IsLoading { get; set; } = false;
        private int SelectedTask { get; set; }
        protected bool? IsSolvedSelectorValue { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this.GetTasks();
        }

        private async Task GetTasks()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.TaskList = await this.TaskService.GetDate(this.IsSolvedSelectorValue);
            this.IsLoading = false;
            this.StateHasChanged();
        }

        protected async void IsSolvedChanged(bool newValue, int taskId, TaskDateDto group)
        {
            var task = this.TaskList.SelectMany(x => x.TaskList).FirstOrDefault(x => x.Id == taskId);

            var taskData = await this.TaskService.Get(taskId);
            if (taskData != null)
            {
                taskData.IsSolved = newValue;
                task.IsSolved = await this.TaskService.Update(taskId, new TaskModel(taskData)) ? newValue : !newValue;
                group.AllSolved = @group.TaskList.Count(x => !x.IsSolved) == 0;
                group.OutOfRange = group.Deadline < DateTime.Now && !group.AllSolved;

                this.StateHasChanged();
            }
        }

        protected void OpenAddTaskModal()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.TaskModalClosed;

            this.Modal.Show<TaskDialog>("Create Task", parameters, options);
        }

        protected void OpenUpdateTaskModal(int taskId)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("task", taskId);

            var options = new ModalOptions();
            options.ButtonOptions.ConfirmButtonType = ConfirmButton.Save;
            options.ButtonOptions.ShowConfirmButton = true;

            this.Modal.OnClose += this.TaskModalClosed;

            this.Modal.Show<TaskDialog>("Update Task", parameters, options);
        }

        protected async void TaskModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetTasks();
            }

            this.Modal.OnClose -= this.TaskModalClosed;
        }

        protected void OpenDeleteModal(TaskDto task)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", task.Title);
            this.SelectedTask = task.Id;

            var options = new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            this.Modal.OnClose += this.DeleteDialogClosed;
            this.Modal.Show<Confirm>("Task Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.TaskService.Delete(this.SelectedTask))
            {
                await this.GetTasks();
            }
            this.Modal.OnClose -= this.DeleteDialogClosed;
        }

        protected async void IsSolvedSelectorValueChanged(bool? value)
        {
            this.IsSolvedSelectorValue = value;
            await this.GetTasks();
        }
    }
}