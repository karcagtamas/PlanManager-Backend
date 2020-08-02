using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class Books
    {
        [Inject] private IBookService BookService { get; set; }

        [Inject] private IHelperService HelperService { get; set; }
        private List<BookListDto> BookList { get; set; }
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData {PropertyName = "Name", DisplayName = "Name", IsSortable = false},
            new TableHeaderData {PropertyName = "Publish", DisplayName = "Publish", IsSortable = false},
            new TableHeaderData {PropertyName = "Author", DisplayName = "Author", IsSortable = false},
            new TableHeaderData {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false}
        };

        private List<string> Footer { get; set; } = new List<string> {" ", " ", " ", " "};

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
            this.Footer[0] = this.BookList.Count().ToString();
            StateHasChanged();
        }
    }
}