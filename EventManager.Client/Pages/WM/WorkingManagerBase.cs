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

        protected WorkingDayModel WorkingDay { get; set; }
        protected int? WorkingDayId { get; set; }
        protected bool IsLoading { get; set; }
        protected List<WorkingDayTypeDto> WorkingDayTypes { get; set; }
        protected List<WorkingFieldListDto> WorkingFields { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetWorkingDay();
            await this.GetWorkingDayTypes();
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetWorkingDay();
        }

        protected async Task GetWorkingDay()
        {
            this.IsLoading = true;
            StateHasChanged();
            var workingDay = await this.WorkingManagerService.GetWorkingDay(this.Date);
            this.WorkingDay = workingDay != null ? new WorkingDayModel
            {
                StartHour = workingDay.StartHour,
                EndHour = workingDay.EndHour,
                StartMin = workingDay.StartMin,
                EndMin = workingDay.EndMin,
                Type = workingDay.Type
            } : null;
            this.WorkingDayId = workingDay != null ? workingDay.Id : (int?)null;
            this.WorkingFields = workingDay != null ? workingDay.WorkingFields : null;
            this.IsLoading = false;
            StateHasChanged();
        }

        protected async Task GetWorkingDayTypes()
        {
            this.WorkingDayTypes = await this.WorkingManagerService.GetWorkingDayTypes();
        }

        protected void Redirect(bool direction)
        {
            this.NavigationManager.NavigateTo($"/wm/{this.HelperService.DateToNumberDayString(this.Date.AddDays(direction ? 1 : -1))}");
        }

        protected async Task InitWorkingDay()
        {
            var workingDay = new WorkingDayInitModel
            {
                Date = Date
            };

            if (await this.WorkingManagerService.CreateWorkingDay(workingDay))
            {
                await this.GetWorkingDay();
            }
        }

        protected async Task Save()
        {
            if (this.WorkingDay != null && await this.WorkingManagerService.UpdateWorkingDay((int)this.WorkingDayId, this.WorkingDay))
            {
                await this.GetWorkingDay();
            }
        }
    }
}
