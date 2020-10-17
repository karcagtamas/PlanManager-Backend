using EventManager.Client.Enums;
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
    public partial class SeriesCategoryDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private ISeriesCategoryService SeriesCategoryService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private int SeriesId { get; set; }
        private List<SeriesCategorySelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData<SeriesCategorySelectorListDto>> Header { get; set; } = new List<TableHeaderData<SeriesCategorySelectorListDto>>
        {
            new TableHeaderData<SeriesCategorySelectorListDto>("Name", "Category", Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.SeriesId = this.Parameters.Get<int>("series");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsSelected).Select(x => x.Id).ToList();

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.List = await this.SeriesCategoryService.GetSelectorList(this.SeriesId);
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private async void OnConfirm()
        {
            var indexList = this.List.Where(x => x.IsSelected).Select(x => x.Id).ToList();

            if (await this.SeriesService.UpdateCategories(this.SeriesId, new SeriesCategoryUpdateModel { Ids = indexList }))
            {
                this.ModalService.Close(ModalResult.Ok(true));
                ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
            }
        }

        private void SwitchSelectedFlag(SeriesCategorySelectorListDto category)
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