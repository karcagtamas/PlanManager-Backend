﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class MovieSelectorDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private List<MyMovieSelectorListDto> List { get; set; }
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData {PropertyName = "Title", DisplayName = "Title", IsSortable = false},
            new TableHeaderData {PropertyName = "Year", DisplayName = "Year", IsSortable = false},
            new TableHeaderData {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false}
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " "};

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");

            await this.GetSelectorList();

            ((ModalService) ModalService).OnConfirm += OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.List = await this.MovieService.GetMySelectorList();
            this.IsLoading = false;
            StateHasChanged();
        }

        private async void OnConfirm()
        {
            var indexList = this.List.Where(x => x.IsMine).Select(x => x.Id).ToList();

            if (await this.MovieService.UpdateMyMovies(new MyMovieModel {Ids = indexList}))
            {
                ModalService.Close(ModalResult.Ok(true));
                ((ModalService) ModalService).OnConfirm -= OnConfirm;
            }
        }

        private void SwitchMineFlag(MyMovieSelectorListDto movie)
        {
            movie.IsMine = !movie.IsMine;
        }
    }
}