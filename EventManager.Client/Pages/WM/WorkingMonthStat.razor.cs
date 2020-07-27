using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.WM
{
    public partial class WorkingMonthStat
    {
        [Parameter] 
        public int Year { get; set; }

        [Parameter] 
        public int Month { get; set; }

        [Inject] 
        private IHelperService HelperService { get; set; }

        [Inject] 
        private NavigationManager NavigationManager { get; set; }
    }
}