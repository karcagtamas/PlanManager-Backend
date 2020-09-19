using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.WM
{
    public partial class WorkingMonthStatPage
    {
        [Parameter]
        public int Year { get; set; }

        [Parameter]
        public int Month { get; set; }

        [Inject]
        private IHelperService HelperService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IWorkingFieldService FieldService { get; set; }

        private WorkingMonthStatDto MonthStat { get; set; }
        private bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this.GetMonthStat();
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetMonthStat();
        }

        private async Task GetMonthStat()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.MonthStat = await this.FieldService.GetMonthStat(this.Year, this.Month);
            this.IsLoading = false;
            StateHasChanged();
        }

        private void Redirect(bool direction)
        {
            if (direction)
            {
                var month = this.Month == 11 ? 0 : this.Month++;
                var year = month == 0 ? this.Year++ : this.Year;
                this.NavigationManager.NavigateTo($"/wm/month/{year}/{month}");
            }
            else
            {
                var month = this.Month == 0 ? 11 : this.Month--;
                var year = month == 11 ? this.Year-- : this.Year;
                this.NavigationManager.NavigateTo($"/wm/month/{year}/{month}");
            }
        }
    }
}