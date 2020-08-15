using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData
                {PropertyName = "Title", DisplayName = "Title", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
            {
                PropertyName = "Year", DisplayName = "Year", IsSortable = false,
                Displaying = (e) => ((int) e).ToString()
            },
            new TableHeaderData
                {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false, Displaying = (e) => (string) e}
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " "};

        protected override async Task OnInitializedAsync()
        {
            await GetMovies();
        }

        private async Task GetMovies()
        {
            IsLoading = true;
            StateHasChanged();
            MovieList = await MovieService.GetAll();
            IsLoading = false;
            Footer[0] = MovieList.Count().ToString();
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