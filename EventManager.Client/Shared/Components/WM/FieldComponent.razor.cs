using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.WM
{
    public partial class FieldComponent
    {
        [Parameter]
        public WorkingFieldListDto WorkingField { get; set; }

        [Parameter]
        public int WorkingDayId { get; set; }

        [Parameter]
        public bool IsModifiable { get; set; } = false;

        [Parameter]
        public EventCallback Close { get; set; }

        [Inject]
        private IModalService Modal { get; set; }

        private void OpenUpdateFieldModal()
        {
            if (this.IsModifiable)
            {
                var parameters = new ModalParameters();
                parameters.Add("FormId", 1);
                parameters.Add("working-day", this.WorkingDayId);
                parameters.Add("field", this.WorkingField.Id);

                var options = new ModalOptions
                {
                    ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
                };

                this.Modal.OnClose += this.FieldModalClosed;

                this.Modal.Show<FieldModal>("Update Working Field", parameters, options);
            }
        }

        private async void FieldModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.Close.InvokeAsync(null);
            }

            this.Modal.OnClose -= this.FieldModalClosed;
        }
    }
}