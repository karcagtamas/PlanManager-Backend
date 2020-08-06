using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
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

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData {PropertyName = "Title", DisplayName = "Title", IsSortable = false},
            new TableHeaderData {PropertyName = "Year", DisplayName = "Year", IsSortable = false},
            new TableHeaderData {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false},
            new TableHeaderData {PropertyName = "Seen", DisplayName = "Seen", IsSortable = false}
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " ", " "};

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
            Footer[0] = MovieList.Count().ToString();
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
    }
}