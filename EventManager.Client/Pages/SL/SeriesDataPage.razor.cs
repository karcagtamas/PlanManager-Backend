using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class SeriesDataPage
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
        private List<int> RateList { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        private int SelectedId { get; set; }
        private bool CanAddOrEdit { get; set; }
        private bool CanDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetSeries();
            await this.GetComments();
            this.CanAddOrEdit = await this.Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
            this.CanDelete = await this.Auth.HasRole("Administrator",
                "Status Library Administrator", "Root");
        }

        private async Task GetSeries()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.Series = await this.SeriesService.GetMy(this.Id);
            if (this.Series.ImageData.Length != 0)
            {
                string base64 = Convert.ToBase64String(this.Series.ImageData);
                this.SeriesImage = $"data:image/gif;base64,{base64}";
            }

            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async Task GetComments()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.CommentList = await this.SeriesCommentService.GetList(this.Id);
            this.IsLoading = false;
            this.StateHasChanged();
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

            this.Modal.OnClose += this.EditSeriesDialogClosed;

            this.Modal.Show<SeriesDialog>("Edit Series", parameters, options);
        }

        private async void EditSeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.EditSeriesDialogClosed;
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
                new List<SeasonSeenStatusModel> { new SeasonSeenStatusModel { Id = season, Seen = status } }))
            {
                await this.GetSeries();
            }
        }

        private async void SetEpisodeSeenStatus(int episode, bool status)
        {
            if (await this.EpisodeService.UpdateSeenStatus(
                new List<EpisodeSeenStatusModel> { new EpisodeSeenStatusModel { Id = episode, Seen = status } }))
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
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditSeriesCategoriesDialogClosed;

            this.Modal.Show<SeriesCategoryDialog>("Edit Categories", parameters, options);
        }

        private async void EditSeriesCategoriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.EditSeriesCategoriesDialogClosed;
        }

        private async void SaveComment()
        {
            if (string.IsNullOrEmpty(this.Comment))
            {
                return;
            }

            if (!await this.SeriesCommentService.Create(new SeriesCommentModel
            { Comment = this.Comment, SeriesId = this.Id }))
            {
                return;
            }

            this.Comment = "";
            await this.GetComments();
        }

        private async void UpdateRate(int rate)
        {
            if (await this.SeriesService.UpdateRate(this.Id, new SeriesRateModel { Rate = rate }))
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
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditSeriesImageDialogClosed;

            this.Modal.Show<SeriesImageDialog>("Edit Series Image", parameters, options);
        }

        private async void EditSeriesImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.EditSeriesImageDialogClosed;
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Series.Title);

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            this.Modal.OnClose += this.DeleteDialogClosed;
            this.Modal.Show<Confirm>("Series Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.SeriesService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("series");
            }

            this.Modal.OnClose -= this.DeleteDialogClosed;
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

            this.Modal.OnClose += this.DeleteSeasonDialogClosed;
            this.Modal.Show<Confirm>("Season Delete", parameters, options);
        }

        private async void DeleteSeasonDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.SeasonService.DeleteDecremented(this.SelectedId))
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.DeleteSeasonDialogClosed;
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

            this.Modal.OnClose += this.DeleteEpisodeDialogClosed;
            this.Modal.Show<Confirm>("Episode Delete", parameters, options);
        }

        private async void DeleteEpisodeDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.EpisodeService.DeleteDecremented(this.SelectedId))
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.DeleteEpisodeDialogClosed;
        }
    }
}