﻿using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class BookSelectorDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IBookService BookService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private List<MyBookSelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData<MyBookSelectorListDto>> Header { get; set; } = new List<TableHeaderData<MyBookSelectorListDto>>
        {
            new TableHeaderData<MyBookSelectorListDto>("Name", Alignment.Left),
            new TableHeaderData<MyBookSelectorListDto>("Publish", "Publish", (e) => DateHelper.DateToString((DateTime?) e), Alignment.Left),
            new TableHeaderData<MyBookSelectorListDto>("Author", Alignment.Left),
            new TableHeaderData<MyBookSelectorListDto>("Creator", Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsMine).Select(x => x.Id).ToList();

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.List = await this.BookService.GetMySelectorList(false);
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async void OnConfirm()
        {
            var indexList = this.List.Where(x => x.IsMine).Select(x => x.Id).ToList();

            if (await this.BookService.UpdateMyBooks(new MyBookModel { Ids = indexList }))
            {
                this.ModalService.Close(ModalResult.Ok(true));
                ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
            }
        }

        private void SwitchMineFlag(MyBookSelectorListDto book)
        {
            book.IsMine = !book.IsMine;
            if (book.IsMine)
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