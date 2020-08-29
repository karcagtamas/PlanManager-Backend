using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class MySeries
    {
        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private List<MySeriesListDto> SeriesList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData("Title"),
            new TableHeaderData("StartYear", "Start Year", (e) => WriteHelper.WriteNullableField((int?) e)),
            new TableHeaderData("Creator")
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " "};

        protected override async Task OnInitializedAsync()
        {
            await GetMovies();
        }

        private async Task GetMovies()
        {
            IsLoading = true;
            StateHasChanged();
            SeriesList = await SeriesService.GetMyList();
            IsLoading = false;
            Footer[0] = SeriesList.Count().ToString();
            StateHasChanged();
        }

        private void RedirectToData(MySeriesListDto series)
        {
            Navigation.NavigateTo($"/series/{series.Id}");
        }

        private void OpenEditMySeriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += EditMySeriesDialogClosed;

            Modal.Show<SeriesSelectorDialog>("Edit My Series", parameters, options);
        }

        private async void EditMySeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetMovies();

            Modal.OnClose -= EditMySeriesDialogClosed;
        }
    }
}