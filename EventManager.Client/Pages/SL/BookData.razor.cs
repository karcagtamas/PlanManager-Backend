using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class BookData
    {
        [Parameter] public int Id { get; set; }

        private BookDto Book { get; set; }

        [Inject] private IBookService BookService { get; set; }

        [Inject] private NavigationManager Navigation { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetBook();
        }

        private async Task GetBook()
        {
            IsLoading = true;
            StateHasChanged();
            Book = await BookService.Get(Id);
            IsLoading = false;
            StateHasChanged();
        }
    }
}