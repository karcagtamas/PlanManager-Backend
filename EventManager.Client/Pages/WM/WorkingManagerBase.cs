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

        protected WorkingDayListDto WorkingDay { get; set; }
        protected bool IsLoading { get; set; } 

        protected override async Task OnInitializedAsync()
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
    }
}
