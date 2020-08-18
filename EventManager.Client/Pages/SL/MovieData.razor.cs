using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class MovieData
    {
        [Parameter] public int Id { get; set; }

        private MyMovieDto Movie { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private IMovieCommentService MovieCommentService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        private bool IsLoading { get; set; }
        private string MovieImage { get; set; }
        private List<MovieCommentListDto> CommentList { get; set; }
        private string Comment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetMovie();
            await GetComments();
        }

        private async Task GetMovie()
        {
            IsLoading = true;
            StateHasChanged();
            this.Movie = await MovieService.GetMy(this.Id);
            if (this.Movie.ImageData.Length != 0)
            {
                var base64 = Convert.ToBase64String(this.Movie.ImageData);
                this.MovieImage = $"data:image/gif;base64,{base64}";
            }

            IsLoading = false;
            StateHasChanged();
        }

        private async Task GetComments() 
        {
            IsLoading = true;
            StateHasChanged();
            this.CommentList = await MovieCommentService.GetList(this.Id);
            IsLoading = false;
            StateHasChanged();
        }

        private void OpenEditMovieDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("movie", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += MovieDialogClosed;

            Modal.Show<MovieDialog>("Edit Movie", parameters, options);
        }

        private async void MovieDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovie();

            Modal.OnClose -= MovieDialogClosed;
        }

        private void OpenEditMovieImageDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("movie", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditMovieImageDialogClosed;

            Modal.Show<MovieImageDialog>("Edit Movie Image", parameters, options);
        }

        private async void EditMovieImageDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovie();

            Modal.OnClose -= EditMovieImageDialogClosed;
        }

        private async void DeleteMovie()
        {
            if (await this.MovieService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("movies");
            }
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
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditMovieCategoriesDialogClosed;

            Modal.Show<MovieCategoryDialog>("Edit Categories", parameters, options);
        }

        private async void EditMovieCategoriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovie();

            Modal.OnClose -= EditMovieCategoriesDialogClosed;
        }

        private async void SaveComment()
        {
            if(string.IsNullOrEmpty(this.Comment)) return;

            if (!await this.MovieCommentService.Create(new MovieCommentModel {Comment = this.Comment, MovieId = this.Id})) return;
            this.Comment = "";
            await this.GetComments();
        }
    }
}