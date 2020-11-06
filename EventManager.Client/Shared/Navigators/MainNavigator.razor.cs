using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Navigators
{
    public partial class MainNavigator
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public IHelperService HelperService { get; set; }

        private string GetWorkingManagerUri()
        {
            return $"/wm/{DateHelper.DateToNumberDayString(DateTime.Now)}";
        }

        private string GetWorkingMonthStatUri()
        {
            return $"/wm/month/{HelperService.CurrentYear()}/{HelperService.CurrentMonth()}";
        }

        private string GetWorkingWeekStatUri()
        {
            return $"/wm/week/{DateHelper.DateToNumberDayString(HelperService.CurrentWeek())}";
        }
    }
}
