using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class BookReadSelectorDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IBookService BookService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private List<MyBookSelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;
        private List<BookReadStatusModel> SaveList = new List<BookReadStatusModel>();

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData
                {PropertyName = "Name", DisplayName = "Name", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
            {
                PropertyName = "Publish", DisplayName = "Publish", IsSortable = false,
                Displaying = (e) => DateHelper.DateToString((DateTime?) e)
            },
            new TableHeaderData
                {PropertyName = "Author", DisplayName = "Author", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
                {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false, Displaying = (e) => (string) e}
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsRead).Select(x => x.Id).ToList();

            ((ModalService) ModalService).OnConfirm += OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.List = await this.BookService.GetMySelectorList(true);
            this.IsLoading = false;
            StateHasChanged();
        }

        private async void OnConfirm()
        {
            if (await this.BookService.UpdateReadStatuses(this.SaveList))
            {
                ModalService.Close(ModalResult.Ok(true));
                ((ModalService) ModalService).OnConfirm -= OnConfirm;
            }
        }

        private void SwitchReadFlag(MyBookSelectorListDto book)
        {
            book.IsRead = !book.IsRead;
            this.SaveList.Add(new BookReadStatusModel {Id = book.Id, Read = book.IsRead});
            if (book.IsRead)
            {
                this.SelectedIndexList.Add(book.Id);
            }
            else
            {
                this.SelectedIndexList.Remove(book.Id);
            }
        }
    }
}