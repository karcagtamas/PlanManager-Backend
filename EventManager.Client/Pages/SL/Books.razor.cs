using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class Books
    {
        [Inject]
        private IBookService BookService { get; set; }
        private List<BookListDto> BookList { get; set; }
        private bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this.GetBooks();
        }

        private async Task GetBooks()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.BookList = await this.BookService.GetAll();
            this.IsLoading = false;
            StateHasChanged();
        }
    }
}