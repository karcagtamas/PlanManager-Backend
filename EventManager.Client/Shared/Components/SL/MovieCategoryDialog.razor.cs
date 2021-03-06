﻿using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class MovieCategoryDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private IMovieCategoryService MovieCategoryService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private int MovieId { get; set; }
        private List<MovieCategorySelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData<MovieCategorySelectorListDto>> Header { get; set; } = new List<TableHeaderData<MovieCategorySelectorListDto>>
        {
            new TableHeaderData<MovieCategorySelectorListDto>("Name", "Category", Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.MovieId = this.Parameters.Get<int>("movie");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsSelected).Select(x => x.Id).ToList();

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.List = await this.MovieCategoryService.GetSelectorList(this.MovieId);
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async void OnConfirm()
        {
            var indexList = this.List.Where(x => x.IsSelected).Select(x => x.Id).ToList();

            if (await this.MovieService.UpdateCategories(this.MovieId, new MovieCategoryUpdateModel { Ids = indexList }))
            {
                this.ModalService.Close(ModalResult.Ok(true));
                ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
            }
        }

        private void SwitchSelectedFlag(MovieCategorySelectorListDto category)
        {
            category.IsSelected = !category.IsSelected;
            if (category.IsSelected)
            {
                this.SelectedIndexList.Add(category.Id);
            }
            else
            {
                this.SelectedIndexList.Remove(category.Id);
            }
        }
    }
}