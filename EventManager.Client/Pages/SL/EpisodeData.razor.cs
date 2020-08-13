using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class EpisodeData
    {
        [Parameter] public int Id { get; set; }

        private MyEpisodeDto Episode { get; set; }
        [Inject] private IEpisodeService EpisodeService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEpisode();
        }

        private async Task GetEpisode()
        {
            IsLoading = true;
            StateHasChanged();
            this.Episode = await EpisodeService.GetMy(this.Id);
            IsLoading = false;
            StateHasChanged();
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
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EpisodeDialogClosed;

            Modal.Show<EpisodeDialog>("Edit Episode", parameters, options);
        }

        private async void EpisodeDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetEpisode();

            Modal.OnClose -= EpisodeDialogClosed;
        }

        private async void SetSeenStatus(bool status)
        {
            if (await this.EpisodeService.UpdateSeenStatus(
                new List<EpisodeSeenStatusModel> {new EpisodeSeenStatusModel {Id = this.Id, Seen = status}}))
            {
                await this.GetEpisode();
            }
        }
    }
}