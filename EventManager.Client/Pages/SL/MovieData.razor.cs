using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class MovieData
    {
        [Parameter] public int Id { get; set; }

        private MyMovieDto Movie { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IHelperService HelperService { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetMovie();
        }

        private async Task GetMovie()
        {
            IsLoading = true;
            StateHasChanged();
            this.Movie = await MovieService.GetMy(this.Id);
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

        private async void DeleteBook() 
        {
            if (await this.MovieService.Delete(this.Id)) {
                this.Navigation.NavigateTo("books");
            }
        }

        private async void AddToMyBooks() 
        {
            if (await this.MovieService.AddMovieToMyMovies(this.Id)) {
                await this.GetMovie();
            }
        }

        private async void RemoveFromMyBooks() 
        {
            if (await this.MovieService.RemoveMovieFromMyMovies(this.Id)) {
                await this.GetMovie();
            }
        }
    }
}