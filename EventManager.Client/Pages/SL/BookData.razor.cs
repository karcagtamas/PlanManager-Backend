using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class BookData
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IBookService BookService { get; set; }
        private bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync() {
            await this.GetBook();
        }

        protected async Task GetBook() 
        {
            this.IsLoading = true;
            StateHasChanged();
            await this.BookService.Get(this.Id);
            this.IsLoading = false;
            StateHasChanged();
        }
    }
}