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
    }
}
