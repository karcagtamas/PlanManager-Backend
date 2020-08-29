using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class MyMovies
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
            new TableHeaderData<MyMovieListDto>("Seen", "Seen", true, (e) => (bool) e ? "Seen" : "Not seen", Alignment.Center)
        };

        protected override async Task OnInitializedAsync()
        {
            await GetMovies();
        }

        private async Task GetMovies()
        {
            IsLoading = true;
            StateHasChanged();
            MovieList = await MovieService.GetMyList();
            IsLoading = false;
            StateHasChanged();
        }

        private void RedirectToData(MyMovieListDto book)
        {
            Navigation.NavigateTo($"/movies/{book.Id}");
        }

        private void OpenEditMyMoviesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditMyMoviesModalClosed;

            Modal.Show<MovieSelectorDialog>("Edit My Books", parameters, options);
        }

        private async void EditMyMoviesModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovies();

            Modal.OnClose -= EditMyMoviesModalClosed;
        }

        private void OpenEditSeenMoviesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditSeenMoviesModalClosed;

            Modal.Show<MovieSeenSelectorDialog>("Edit Seen Books", parameters, options);
        }

        private async void EditSeenMoviesModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovies();

            Modal.OnClose -= EditSeenMoviesModalClosed;
        }
    }
}