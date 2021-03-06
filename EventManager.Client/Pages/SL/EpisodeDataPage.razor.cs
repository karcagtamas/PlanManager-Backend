using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class EpisodeDataPage
    {
        [Parameter] public int Id { get; set; }

        private MyEpisodeDto Episode { get; set; }
        [Inject] private IEpisodeService EpisodeService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IAuthService Auth { get; set; }

        private bool IsLoading { get; set; }
        private string EpisodeImage { get; set; }
        private bool CanEdit { get; set; }
        private bool CanDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetEpisode();
            this.CanEdit = await this.Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
            this.CanDelete = await this.Auth.HasRole("Administrator",
                "Status Library Administrator", "Root");
        }

        private async Task GetEpisode()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.Episode = await this.EpisodeService.GetMy(this.Id);
            if (this.Episode.ImageData.Length != 0)
            {
                string base64 = Convert.ToBase64String(this.Episode.ImageData);
                this.EpisodeImage = $"data:image/gif;base64,{base64}";
            }

            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async void DeleteDecremented()
        {
            if (await this.EpisodeService.DeleteDecremented(this.Id))
            {
                this.Navigation.NavigateTo("/series");
            }
        }

        private void OpenEditEpisodeDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("episode", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EpisodeDialogClosed;

            this.Modal.Show<EpisodeDialog>("Edit Episode", parameters, options);
        }

        private async void EpisodeDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetEpisode();
            }

            this.Modal.OnClose -= this.EpisodeDialogClosed;
        }

        private async void SetSeenStatus(bool status)
        {
            if (await this.EpisodeService.UpdateSeenStatus(
                new List<EpisodeSeenStatusModel> { new EpisodeSeenStatusModel { Id = this.Id, Seen = status } }))
            {
                await this.GetEpisode();
            }
        }

        private void OpenEditEpisodeImageDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("episode", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditEpisodeImageDialogClosed;

            this.Modal.Show<EpisodeImageDialog>("Edit Episode Image", parameters, options);
        }

        private async void EditEpisodeImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetEpisode();
            }

            this.Modal.OnClose -= this.EditEpisodeImageDialogClosed;
        }
    }
}