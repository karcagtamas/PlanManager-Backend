using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class BookData
    {
        [Parameter] public int Id { get; set; }
        private MyBookDto Book { get; set; }
        [Inject] private IBookService BookService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IHelperService HelperService { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetBook();
        }

        private async Task GetBook()
        {
            IsLoading = true;
            StateHasChanged();
            Book = await BookService.GetMy(this.Id);
            IsLoading = false;
            StateHasChanged();
        }

        private void OpenEditBookDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("book", this.Id);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += BookModalClosed;

            Modal.Show<BookDialog>("Edit Book", parameters, options);
        }

        private async void BookModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetBook();

            Modal.OnClose -= BookModalClosed;
        }

        private async void DeleteBook() 
        {
            if (await this.BookService.Delete(this.Id)) {
                this.Navigation.NavigateTo("books");
            }
        }

        private async void AddToMyBooks() 
        {
            if (await this.BookService.AddBookToMyBooks(this.Id)) {
                await this.GetBook();
            }
        }

        private async void RemoveFromMyBooks() 
        {
            if (await this.BookService.RemoveBookFromMyBooks(this.Id)) {
                await this.GetBook();
            }
        }
    }
}