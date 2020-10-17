using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class BookListPage
    {
        [Inject] private IBookService BookService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IAuthService Auth { get; set; }

        private List<BookListDto> BookList { get; set; }
        private bool IsLoading { get; set; }
        private bool CanAdd { get; set; }

        private List<TableHeaderData<BookListDto>> Header { get; set; } = new List<TableHeaderData<BookListDto>>
        {
            new TableHeaderData<BookListDto>("Name", true, Alignment.Left)
                {FooterRunnableData = (list) => list.Count.ToString()},
            new TableHeaderData<BookListDto>("Publish", "Publish", true, (e) => DateHelper.DateToString((DateTime?) e),
                Alignment.Right),
            new TableHeaderData<BookListDto>("Author", true, Alignment.Left),
            new TableHeaderData<BookListDto>("Creator", true, Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            await this.GetBooks();
            this.CanAdd = await this.Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
        }

        private async Task GetBooks()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.BookList = await this.BookService.GetAll("Name");
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void RedirectToData(BookListDto book)
        {
            this.Navigation.NavigateTo($"/books/{book.Id}");
        }

        private void OpenAddBookDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.BookModalClosed;

            this.Modal.Show<BookDialog>("Create Book", parameters, options);
        }

        private async void BookModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetBooks();
            }

            this.Modal.OnClose -= this.BookModalClosed;
        }
    }
}