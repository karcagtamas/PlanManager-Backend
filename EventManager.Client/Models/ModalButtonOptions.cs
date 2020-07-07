using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models
{
    public class ModalButtonOptions
    {
        public bool ShowCancelButton { get; set; } = true;
        public bool ShowConfirmButton { get; set; } = false;
        public CancelButton CancelButtonType { get; set; } = CancelButton.Cancel;
        public ConfirmButton ConfirmButtonType { get; set; } = ConfirmButton.Ok;

        public ModalButtonOptions() { }

        public ModalButtonOptions(bool showCancel, bool showConfirm) 
        {
            this.ShowCancelButton = showCancel;
            this.ShowConfirmButton = showConfirm;
        }

        public ModalButtonOptions(bool showCancel, bool showConfirm, CancelButton cancelButton, ConfirmButton confirmButton)
        {
            this.ShowCancelButton = showCancel;
            this.ShowConfirmButton = showConfirm;
            this.CancelButtonType = cancelButton;
            this.ConfirmButtonType = confirmButton;
        }
    }
}
