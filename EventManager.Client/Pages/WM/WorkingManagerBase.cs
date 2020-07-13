using EventManager.Client.Models.WM;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.WM
{
    public class WorkingManagerBase : ComponentBase
    {
        [Parameter]
        public DateTime Date { get; set; }

        [Inject]
        private IWorkingManagerService WorkingManagerService { get; set; }

        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected WorkingDayListDto WorkingDay { get; set; }
        protected bool IsLoading { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            await this.GetWorkingDay();
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetWorkingDay();
        }

        protected async Task GetWorkingDay()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.WorkingDay = await this.WorkingManagerService.GetWorkingDay(this.Date);
            this.IsLoading = false;
            StateHasChanged();
        }

        protected void Redirect(bool direction)
        {
            this.NavigationManager.NavigateTo($"/wm/{this.HelperService.DateToNumberDayString(this.Date.AddDays(direction ? 1 : -1))}");
        }

        protected async Task InitWorkingDay()
        {
            var workingDay = new WorkingDayDto
            {
                Day = Date,
                StartHour = 8,
                StartMin = 0,
                EndHour = 16,
                EndMin = 0,
                Type = 1
            };

            if (await this.WorkingManagerService.CreateWorkingDay(workingDay))
            {
                await this.GetWorkingDay();
            }
        }
    }
}
