using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class BookDataPage
    {
        [Parameter] public int Id { get; set; }
        private MyBookDto Book { get; set; }
        [Inject] private IBookService BookService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }
        [Inject] private IAuthService Auth { get; set; }

        private bool IsLoading { get; set; }
        private bool CanEdit { get; set; }
        private bool CanDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetBook();
            this.CanEdit = await Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
            this.CanDelete = await Auth.HasRole("Administrator",
                "Status Library Administrator", "Root");
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
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            Modal.OnClose += BookModalClosed;

            Modal.Show<BookDialog>("Edit Book", parameters, options);
        }

        private async void BookModalClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data) await GetBook();

            Modal.OnClose -= BookModalClosed;
        }

        private async void AddToMyBooks()
        {
            if (await this.BookService.AddBookToMyBooks(this.Id))
            {
                await this.GetBook();
            }
        }

        private async void RemoveFromMyBooks()
        {
            if (await this.BookService.RemoveBookFromMyBooks(this.Id))
            {
                await this.GetBook();
            }
        }

        private async void SetReadStatus(bool status)
        {
            if (await this.BookService.UpdateReadStatuses(new List<BookReadStatusModel>
                {new BookReadStatusModel {Id = this.Book.Id, Read = status}}))
            {
                await this.GetBook();
            }
        }

        private void OpenDeleteDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", ConfirmType.Delete);
            parameters.Add("name", this.Book.Name);

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += DeleteDialogClosed;
            Modal.Show<Confirm>("Book Delete", parameters, options);
        }

        private async void DeleteDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && await BookService.Delete(this.Id))
            {
                this.Navigation.NavigateTo("books");
            }

            Modal.OnClose -= DeleteDialogClosed;
        }
    }
}