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
    public partial class MovieDataPage
    {
        [Parameter] public int Id { get; set; }

        private MyMovieDto Movie { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private IMovieCommentService MovieCommentService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IAuthService Auth { get; set; }
        private bool IsLoading { get; set; }
        private string MovieImage { get; set; }
        private List<MovieCommentListDto> CommentList { get; set; }
        private string Comment { get; set; }
        private List<int> RateList { get; set; } = new List<int> { 1, 2, 3, 4, 5 };
        private bool CanEdit { get; set; }
        private bool CanDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetMovie();
            await this.GetComments();
            this.CanEdit = await this.Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
            this.CanDelete = await this.Auth.HasRole("Administrator",
                "Status Library Administrator", "Root");
        }

        private async Task GetMovie()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.Movie = await this.MovieService.GetMy(this.Id);
            if (this.Movie.ImageData.Length != 0)
            {
                string base64 = Convert.ToBase64String(this.Movie.ImageData);
                this.MovieImage = $"data:image/gif;base64,{base64}";
            }

            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async Task GetComments()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.CommentList = await this.MovieCommentService.GetList(this.Id);
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void OpenEditMovieDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("movie", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.MovieDialogClosed;

            this.Modal.Show<MovieDialog>("Edit Movie", parameters, options);
        }

        private async void MovieDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetMovie();
            }

            this.Modal.OnClose -= this.MovieDialogClosed;
        }

        private void OpenEditMovieImageDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("movie", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditMovieImageDialogClosed;

            this.Modal.Show<MovieImageDialog>("Edit Movie Image", parameters, options);
        }

        private async void EditMovieImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetMovie();
            }

            this.Modal.OnClose -= this.EditMovieImageDialogClosed;
        }

        private async void AddToMyMovies()
        {
            if (await this.MovieService.AddMovieToMyMovies(this.Id))
            {
                await this.GetMovie();
            }
        }

        private async void RemoveFromMyMovies()
        {
            if (await this.MovieService.RemoveMovieFromMyMovies(this.Id))
            {
                await this.GetMovie();
            }
        }

        private async void SetSeenStatus(bool status)
        {
            if (await this.MovieService.UpdateSeenStatuses(new List<MovieSeenUpdateModel>
                {new MovieSeenUpdateModel {Id = this.Movie.Id, Seen = status}}))
            {
                await this.GetMovie();
            }
        }

        private void OpenEditMovieCategoriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("movie", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditMovieCategoriesDialogClosed;

            this.Modal.Show<MovieCategoryDialog>("Edit Categories", parameters, options);
        }

        private async void EditMovieCategoriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetMovie();
            }

            this.Modal.OnClose -= this.EditMovieCategoriesDialogClosed;
        }

        private async void SaveComment()
        {
            if (string.IsNullOrEmpty(this.Comment))
            {
                return;
            }

            if (!await this.MovieCommentService.Create(
                new MovieCommentModel { Comment = this.Comment, MovieId = this.Id }))
            {
                return;
            }

            this.Comment = "";
            await this.GetComments();
        }

        private async void UpdateRate(int rate)
        {
            if (await this.MovieService.UpdateRate(this.Id, new MovieRateModel { Rate = rate }))
            {
                await this.GetMovie();
            }
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Movie.Title);

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            this.Modal.OnClose += this.DeleteDialogClosed;
            this.Modal.Show<Confirm>("Movie Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await this.MovieService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("movies");
            }

            this.Modal.OnClose -= this.DeleteDialogClosed;
        }
    }
}