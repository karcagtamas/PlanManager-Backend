using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class MyMovieListPage
    {
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private List<MyMovieListDto> MovieList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData<MyMovieListDto>> Header { get; set; } = new List<TableHeaderData<MyMovieListDto>>
        {
            new TableHeaderData<MyMovieListDto>("Title", true, Alignment.Left)
                {FooterRunnableData = (list) => list.Count.ToString()},
            new TableHeaderData<MyMovieListDto>("ReleaseYear", "Release Year", true, (e) => ((int) e).ToString(), Alignment.Right),
            new TableHeaderData<MyMovieListDto>("Creator", true, Alignment.Left),
            new TableHeaderData<MyMovieListDto>("IsSeen", "Is Seen", true, (e) => ((bool) e) ? "Seen" : "Not seen", Alignment.Center)
        };

        protected override async Task OnInitializedAsync()
        {
            await this.GetMovies();
        }

        private async Task GetMovies()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.MovieList = await this.MovieService.GetMyList();
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void RedirectToData(MyMovieListDto book)
        {
            this.Navigation.NavigateTo($"/movies/{book.Id}");
        }

        private void OpenEditMyMoviesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditMyMoviesModalClosed;

            this.Modal.Show<MovieSelectorDialog>("Edit My Books", parameters, options);
        }

        private async void EditMyMoviesModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetMovies();
            }

            this.Modal.OnClose -= this.EditMyMoviesModalClosed;
        }

        private void OpenEditSeenMoviesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditSeenMoviesModalClosed;

            this.Modal.Show<MovieSeenSelectorDialog>("Edit Seen Books", parameters, options);
        }

        private async void EditSeenMoviesModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetMovies();
            }

            this.Modal.OnClose -= this.EditSeenMoviesModalClosed;
        }
    }
}