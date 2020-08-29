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
    public partial class Movies
    {
        [Inject] private IMovieService MovieService { get; set; }

        [Inject] private NavigationManager Navigation { get; set; }

        [Inject] private IModalService Modal { get; set; }

        private List<MovieListDto> MovieList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData<MovieListDto>> Header { get; set; } = new List<TableHeaderData<MovieListDto>>
        {
            new TableHeaderData<MovieListDto>("Title", true, Alignment.Left)
                {FooterRunnableData = (list) => list.Count.ToString()},
            new TableHeaderData<MovieListDto>("ReleaseYear", "Release Year", true, (e) => ((int) e).ToString(), Alignment.Right),
            new TableHeaderData<MovieListDto>("Creator", true, Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            await GetMovies();
        }

        private async Task GetMovies()
        {
            IsLoading = true;
            StateHasChanged();
            MovieList = await MovieService.GetAll("Title");
            IsLoading = false;
            StateHasChanged();
        }

        private void RedirectToData(MovieListDto movie)
        {
            Navigation.NavigateTo($"/movies/{movie.Id}");
        }

        private void OpenAddMovieDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += MovieModalClosed;

            Modal.Show<MovieDialog>("Create Movie", parameters, options);
        }

        private async void MovieModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovies();

            Modal.OnClose -= MovieModalClosed;
        }
    }
}