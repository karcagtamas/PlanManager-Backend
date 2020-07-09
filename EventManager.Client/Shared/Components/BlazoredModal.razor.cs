using System;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components {
    public partial class BlazoredModal : IDisposable {
        const string defaultStyle = "blazored-modal";
        const string defaultPosition = "blazored-modal-center";

        [Inject]
        private IModalService ModalService { get; set; }

        [Parameter]
        public bool HideHeader { get; set; }

        [Parameter]
        public bool HideCloseButton { get; set; }

        [Parameter]
        public bool DisableBackgroundCancel { get; set; }

        [Parameter]
        public string Position { get; set; }

        [Parameter]
        public string Style { get; set; }

        private bool ComponentDisableBackgroundCancel { get; set; }
        private bool ComponentHideHeader { get; set; }
        private bool ComponentHideCloseButton { get; set; }
        private string ComponentPosition { get; set; }
        private string ComponentStyle { get; set; }
        private bool IsVisible { get; set; }
        private string Title { get; set; }
        private bool ShowCancelButton { get; set; }
        private bool ShowConfirmButton { get; set; }
        private string CancelButtonText { get; set; }
        private string ConfirmButtonText { get; set; }
        private RenderFragment Content { get; set; }
        private ModalParameters Parameters { get; set; }

        protected override void OnInitialized () {
            ((ModalService) ModalService).OnShow += ShowModal;
            ((ModalService) ModalService).CloseModal += CloseModal;
        }

        public void Dispose () {
            Dispose (true);
            GC.SuppressFinalize (this);
        }

        private async void ShowModal (string title, RenderFragment content, ModalParameters parameters, ModalOptions options) {
            this.Title = title;
            this.Content = content;
            this.Parameters = parameters;
            this.IsVisible = true;

            if (options != null)
            {
                this.SetButtonSettings(options);
            }
            await InvokeAsync (StateHasChanged);
        }

        private async void CloseModal () {
            this.Title = "";
            this.Content = null;
            this.ComponentStyle = "";
            IsVisible = false;
            await InvokeAsync (StateHasChanged);
        }

        protected virtual void Dispose (bool disposing) {
            if (disposing) {
                ((ModalService) ModalService).OnShow -= ShowModal;
                ((ModalService) ModalService).CloseModal -= CloseModal;
            }
        }

        private void HandleBackgroundClick (bool value) {
            if (!value)
            {
                if (ComponentDisableBackgroundCancel) {
                    return;
                }
                ModalService.Cancel ();
            }
        }

        private void SetModalOptions (ModalOptions options) {
            ComponentHideHeader = HideHeader;
            if (options.HideHeader.HasValue) {
                ComponentHideHeader = options.HideHeader.Value;
            }

            ComponentHideCloseButton = HideCloseButton;
            if (options.HideCloseButton.HasValue) {
                ComponentHideCloseButton = options.HideCloseButton.Value;
            }

            ComponentDisableBackgroundCancel = DisableBackgroundCancel;
            if (options.DisableBackgroundCancel.HasValue) {
                ComponentDisableBackgroundCancel = options.DisableBackgroundCancel.Value;
            }

            ComponentPosition = string.IsNullOrWhiteSpace (options.Position) ? Position : options.Position;
            if (string.IsNullOrWhiteSpace (ComponentPosition)) {
                ComponentPosition = defaultPosition;
            }

            ComponentStyle = string.IsNullOrWhiteSpace (options.Style) ? Style : options.Style;
            if (string.IsNullOrWhiteSpace (ComponentStyle)) {
                ComponentStyle = defaultStyle;
            }
        }

        public void SetTitle (string title) {
            Title = title;
            StateHasChanged ();
        }

        public void SetButtonSettings (ModalOptions options) 
        {
            this.ShowCancelButton = options.ButtonOptions.ShowCancelButton;
            this.ShowConfirmButton = options.ButtonOptions.ShowConfirmButton;
            this.CancelButtonText = this.GetNameOfButtonType(options.ButtonOptions.CancelButtonType);
            this.ConfirmButtonText = this.GetNameOfButtonType(options.ButtonOptions.ConfirmButtonType);
        }

        public string GetNameOfButtonType(ConfirmButton type)
        {
            return type.ToString();
        }

        public string GetNameOfButtonType(CancelButton type)
        {
            return type.ToString();
        }
    }
}