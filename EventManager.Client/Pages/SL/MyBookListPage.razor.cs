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
    public partial class MyBookListPage
    {
        [Inject] private IBookService BookService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private List<MyBookListDto> BookList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData<MyBookListDto>> Header { get; set; } = new List<TableHeaderData<MyBookListDto>>
        {
            new TableHeaderData<MyBookListDto>("Name", true, Alignment.Left)
                {FooterRunnableData = (list) => list.Count.ToString()},
            new TableHeaderData<MyBookListDto>("Publish", "Publish", true, (e) => DateHelper.DateToString((DateTime?) e),
                Alignment.Right),
            new TableHeaderData<MyBookListDto>("Author", true, Alignment.Left),
            new TableHeaderData<MyBookListDto>("Creator", true, Alignment.Left),
            new TableHeaderData<MyBookListDto>("Read", "Read", true, (e) => (bool) e ? "Read" : "Not read", Alignment.Center)
        };

        protected override async Task OnInitializedAsync()
        {
            await this.GetBooks();
        }

        private async Task GetBooks()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.BookList = await this.BookService.GetMyList();
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void RedirectToData(MyBookListDto book)
        {
            this.Navigation.NavigateTo($"/books/{book.Id}");
        }

        private void OpenEditMyBooksDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditMyBooksModalClosed;

            this.Modal.Show<BookSelectorDialog>("Edit My Books", parameters, options);
        }

        private async void EditMyBooksModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetBooks();
            }

            this.Modal.OnClose -= this.EditMyBooksModalClosed;
        }

        private void OpenEditReadBooksDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.EditReadBooksModalClosed;

            this.Modal.Show<BookReadSelectorDialog>("Edit Read Books", parameters, options);
        }

        private async void EditReadBooksModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetBooks();
            }

            this.Modal.OnClose -= this.EditReadBooksModalClosed;
        }
    }
}