using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class SeriesSelectorDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private List<MySeriesSelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;

        private List<TableHeaderData<MySeriesSelectorListDto>> Header { get; set; } = new List<TableHeaderData<MySeriesSelectorListDto>>
        {
            new TableHeaderData<MySeriesSelectorListDto>("Title", Alignment.Left),
            new TableHeaderData<MySeriesSelectorListDto>("StartYear", "Start Year", (e) => WriteHelper.WriteNullableField((int?) e), Alignment.Right),
            new TableHeaderData<MySeriesSelectorListDto>("Creator", Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsMine).Select(x => x.Id).ToList();

            ((ModalService)ModalService).OnConfirm += OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.List = await this.SeriesService.GetMySelectorList(false);
            this.IsLoading = false;
            StateHasChanged();
        }

        private async void OnConfirm()
        {
            var indexList = this.List.Where(x => x.IsMine).Select(x => x.Id).ToList();

            if (await this.SeriesService.UpdateMySeries(new MySeriesModel { Ids = indexList }))
            {
                ModalService.Close(ModalResult.Ok(true));
                ((ModalService)ModalService).OnConfirm -= OnConfirm;
            }
        }

        private void SwitchMineFlag(MySeriesSelectorListDto series)
        {
            series.IsMine = !series.IsMine;
            if (series.IsMine)
            {
                this.SelectedIndexList.Add(series.Id);
            }
            else
            {
                this.SelectedIndexList.Remove(series.Id);
            }
        }
    }
}