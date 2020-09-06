using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class SeriesData
    {
        [Parameter] public int Id { get; set; }

        private MySeriesDto Series { get; set; }
        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private ISeriesCommentService SeriesCommentService { get; set; }
        [Inject] private ISeasonService SeasonService { get; set; }
        [Inject] private IEpisodeService EpisodeService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IAuthService Auth { get; set; }
        private bool IsLoading { get; set; }
        private string SeriesImage { get; set; }
        private List<SeriesCommentListDto> CommentList { get; set; }
        private string Comment { get; set; }
        private List<int> RateList { get; set; } = new List<int> {1, 2, 3, 4, 5};
        private int SelectedId { get; set; }
        private bool CanAddOrEdit { get; set; }
        private bool CanDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetSeries();
            await GetComments();
            this.CanAddOrEdit = await Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
            this.CanDelete = await Auth.HasRole("Administrator",
                "Status Library Administrator", "Root");
        }

        private async Task GetSeries()
        {
            IsLoading = true;
            StateHasChanged();
            this.Series = await SeriesService.GetMy(this.Id);
            if (this.Series.ImageData.Length != 0)
            {
                var base64 = Convert.ToBase64String(this.Series.ImageData);
                this.SeriesImage = $"data:image/gif;base64,{base64}";
            }

            IsLoading = false;
            StateHasChanged();
        }

        private async Task GetComments()
        {
            IsLoading = true;
            StateHasChanged();
            this.CommentList = await SeriesCommentService.GetList(this.Id);
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
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditSeriesDialogClosed;

            Modal.Show<SeriesDialog>("Edit Series", parameters, options);
        }

        private async void EditSeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetSeries();

            Modal.OnClose -= EditSeriesDialogClosed;
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
                new SeriesSeenStatusModel {Id = this.Series.Id, Seen = status}))
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

        private async void AddIncrementedEpisode(int season)
        {
            if (await this.EpisodeService.AddIncremented(season))
            {
                await this.GetSeries();
            }
        }

        private void OpenEpisode(int id)
        {
            this.Navigation.NavigateTo($"/episodes/{id}");
        }

        private async void SetSeasonSeenStatus(int season, bool status)
        {
            if (await this.SeasonService.UpdateSeenStatus(
                new List<SeasonSeenStatusModel> {new SeasonSeenStatusModel {Id = season, Seen = status}}))
            {
                await this.GetSeries();
            }
        }

        private async void SetEpisodeSeenStatus(int episode, bool status)
        {
            if (await this.EpisodeService.UpdateSeenStatus(
                new List<EpisodeSeenStatusModel> {new EpisodeSeenStatusModel {Id = episode, Seen = status}}))
            {
                await this.GetSeries();
            }
        }

        private void OpenEditSeriesCategoriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("series", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditSeriesCategoriesDialogClosed;

            Modal.Show<SeriesCategoryDialog>("Edit Categories", parameters, options);
        }

        private async void EditSeriesCategoriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetSeries();

            Modal.OnClose -= EditSeriesCategoriesDialogClosed;
        }

        private async void SaveComment()
        {
            if (string.IsNullOrEmpty(this.Comment)) return;

            if (!await this.SeriesCommentService.Create(new SeriesCommentModel
                {Comment = this.Comment, SeriesId = this.Id})) return;
            this.Comment = "";
            await this.GetComments();
        }

        private async void UpdateRate(int rate)
        {
            if (await this.SeriesService.UpdateRate(this.Id, new SeriesRateModel {Rate = rate}))
            {
                await this.GetSeries();
            }
        }

        private void OpenEditSeriesImageDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("series", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditSeriesImageDialogClosed;

            Modal.Show<SeriesImageDialog>("Edit Series Image", parameters, options);
        }

        private async void EditSeriesImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetSeries();

            Modal.OnClose -= EditSeriesImageDialogClosed;
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Series.Title);

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += DeleteDialogClosed;
            Modal.Show<Confirm>("Series Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data && await SeriesService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("series");
            }

            Modal.OnClose -= DeleteDialogClosed;
        }
        
        private void OpenDeleteSeasonDialog(MySeasonDto season)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", season.Number.ToString());
            this.SelectedId = season.Id;

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += DeleteSeasonDialogClosed;
            Modal.Show<Confirm>("Season Delete", parameters, options);
        }

        private async void DeleteSeasonDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data && await SeasonService.DeleteDecremented(this.SelectedId))
            {
                await this.GetSeries();
            }

            Modal.OnClose -= DeleteSeasonDialogClosed;
        }
        
        private void OpenDeleteEpisodeDialog(MyEpisodeListDto episode)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", episode.Title);
            this.SelectedId = episode.Id;

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += DeleteEpisodeDialogClosed;
            Modal.Show<Confirm>("Episode Delete", parameters, options);
        }

        private async void DeleteEpisodeDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data && await EpisodeService.DeleteDecremented(this.SelectedId))
            {
                await this.GetSeries();
            }

            Modal.OnClose -= DeleteEpisodeDialogClosed;
        }
    }
}