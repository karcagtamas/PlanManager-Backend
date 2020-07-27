using System;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.WM
{
    public partial class WorkingWeekStat
    {
        [Parameter]
        public DateTime Week { get; set; }
        
        [Inject] 
        private IHelperService HelperService { get; set; }

        [Inject] 
        private NavigationManager NavigationManager { get; set; }
    }
}