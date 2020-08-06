using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class Books
    {
        [Inject] private IBookService BookService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private List<BookListDto> BookList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData
                {PropertyName = "Name", DisplayName = "Name", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
            {
                PropertyName = "Publish", DisplayName = "Publish", IsSortable = false,
                Displaying = (e) => DateHelper.DateToString((DateTime) e)
            },
            new TableHeaderData
                {PropertyName = "Author", DisplayName = "Author", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
                {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false, Displaying = (e) => (string) e}
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " ", " "};

        protected override async Task OnInitializedAsync()
        {
            await GetBooks();
        }

        private async Task GetBooks()
        {
            IsLoading = true;
            StateHasChanged();
            BookList = await BookService.GetAll();
            IsLoading = false;
            Footer[0] = BookList.Count().ToString();
            StateHasChanged();
        }

        private void RedirectToData(BookListDto book)
        {
            Navigation.NavigateTo($"/books/{book.Id}");
        }

        private void OpenAddBookDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += BookModalClosed;

            Modal.Show<BookDialog>("Create Book", parameters, options);
        }

        private async void BookModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetBooks();

            Modal.OnClose -= BookModalClosed;
        }
    }
}