using EventManager.Client.Models.Tasks;
using EventManager.Client.Services.Interfaces;
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
    }
}
