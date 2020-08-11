using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class SeriesData
    {
        [Parameter] public int Id { get; set; }

        private MySeriesDto Series { get; set; }
        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private ISeasonService SeasonService { get; set; }
        [Inject] private IEpisodeService EpisodeService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetSeries();
        }

        private async Task GetSeries()
        {
            IsLoading = true;
            StateHasChanged();
            this.Series = await SeriesService.GetMy(this.Id);
            IsLoading = false;
            StateHasChanged();
        }

        private void OpenEditSeriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("series", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            Modal.OnClose += EditSeriesDialogClosed;

            Modal.Show<SeriesDialog>("Edit Series", parameters, options);
        }

        private async void EditSeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data) await GetSeries();

            Modal.OnClose -= EditSeriesDialogClosed;
        }

        private async void DeleteSeries()
        {
            if (await this.SeriesService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("series");
            }
        }

        private async void AddToMySeriesList()
        {
            if (await this.SeriesService.AddSeriesToMySeries(this.Id))
            {
                await this.GetSeries();
            }
        }

        private async void RemoveFromMySeriesList()
        {
            if (await this.SeriesService.RemoveSeriesFromMySeries(this.Id))
            {
                await this.GetSeries();
            }
        }

        private async void SetSeenStatus(bool status)
        {
            if (await this.SeriesService.UpdateSeenStatus(
                new SeriesSeenStatusModel { Id = this.Series.Id, Seen = status }))
            {
                await this.GetSeries();
            }
        }

        private async void AddIncrementedSeason()
        {
            if (await this.SeasonService.AddIncremented(this.Series.Id))
            {
                await this.GetSeries();
            }
        }

        private async void DeleteDecrementedSeason(int seasonId)
        {
            if (await this.SeasonService.DeleteDecremented(seasonId))
            {
                await this.GetSeries();
            }
        }

        private async void AddIncrementedEpisode(int season)
        {
            if (await this.EpisodeService.AddIncremented(season))
            {
                await this.GetSeries();
            }
        }

        private async void DeleteDecrementedEpisode(int episodeId)
        {
            if (await this.EpisodeService.DeleteDecremented(episodeId))
            {
                await this.GetSeries();
            }
        }
    }
}