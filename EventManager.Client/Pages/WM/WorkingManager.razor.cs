using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.WM
{
    public partial class WorkingManager
    {
        [Parameter] 
        public DateTime Date { get; set; }

        [Inject]
        private IWorkingDayService WorkingDayService { get; set; }
        
        [Inject]
        private IWorkingDayTypeService WorkingDayTypeService { get; set; }

        [Inject] 
        private IHelperService HelperService { get; set; }

        [Inject] 
        private NavigationManager NavigationManager { get; set; }

        [Inject] 
        private IModalService Modal { get; set; }

        private WorkingDayModel WorkingDay { get; set; }
        private int? WorkingDayId { get; set; }
        private bool IsLoading { get; set; }
        private List<WorkingDayTypeListDto> WorkingDayTypes { get; set; }
        private List<WorkingFieldListDto> WorkingFields { get; set; }
        private WorkingDayStatDto WorkingDayStat { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetWorkingDay();
            await this.GetWorkingDayTypes();
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetWorkingDay();
        }

        private async Task GetWorkingDay()
        {
            this.IsLoading = true;
            StateHasChanged();
            var workingDay = await this.WorkingDayService.Get(this.Date);
            this.WorkingDay = workingDay != null ? new WorkingDayModel
            {
                Type = workingDay.Type,
                Date = workingDay.Day
            } : null;
            this.WorkingDayId = workingDay?.Id;
            this.WorkingFields = workingDay?.WorkingFields;
            this.WorkingDayStat =
                this.WorkingDayId == null ? null : await this.WorkingDayService.Stat((int)this.WorkingDayId);
            this.IsLoading = false;
            StateHasChanged();
        }

        private async Task GetWorkingDayTypes()
        {
            this.WorkingDayTypes = await this.WorkingDayTypeService.GetAll();
        }

        private void Redirect(bool direction)
        {
            this.NavigationManager.NavigateTo($"/wm/{this.HelperService.DateToNumberDayString(this.Date.AddDays(direction ? 1 : -1))}");
        }

        private async Task InitWorkingDay()
        {
            var workingDay = new WorkingDayModel
            {
                Date = this.Date,
                Type = 1
            };

            if (await this.WorkingDayService.Create(workingDay))
            {
                await this.GetWorkingDay();
            }
        }

        private async Task Save()
        {
            if (this.WorkingDay != null && this.WorkingDayId != null && await this.WorkingDayService.Update((int)this.WorkingDayId, this.WorkingDay))
            {
                await this.GetWorkingDay();
            }
        }

        private void OpenAddFieldModal()
        {
            if (this.WorkingDayId != null) {

                var parameters = new ModalParameters();
                parameters.Add("FormId", 1);
                parameters.Add("working-day", (int)this.WorkingDayId);

                var options = new ModalOptions();
                options.ButtonOptions.ConfirmButtonType = ConfirmButton.Save;
                options.ButtonOptions.ShowConfirmButton = true;

                Modal.OnClose += FieldModalClosed;

                Modal.Show<FieldModal>("Create Working Field", parameters, options);
            }
        }

        private void OpenUpdateFieldModal(int fieldId)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            if (this.WorkingDayId != null)
            {
                parameters.Add("working-day", (int)this.WorkingDayId);
            }
            parameters.Add("field", fieldId);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += FieldModalClosed;

            Modal.Show<FieldModal>("Update Working Field", parameters, options);
        }

        private async void FieldModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetWorkingDay();
            }

            Modal.OnClose -= FieldModalClosed;
        }
    }
}
