namespace EventManager.Client.Models {
    public class ModalOptions {
        public string Position { get; set; }
        public string Style { get; set; }
        public bool? DisableBackgroundCancel { get; set; }
        public bool? HideButton { get; set; }
        public bool? HideCloseButton { get; set; }
        public bool? HideHeader { get; set; }
        public bool ShowCancelButton { get; set; } = true;
        public bool ShowConfirmButton { get; set; } = false;
        public CancelButton CancelButtonType { get; set; } = CancelButton.Cancel;
        public ConfirmButton ConfirmButtonType { get; set; } = ConfirmButton.Ok;

    }

    public enum CancelButton
    {
        Cancel,
        Close
    }

    public enum ConfirmButton
    {
        Confirm,
        Save,
        Ok
    }
}