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
    public partial class Series
    {
        [Inject] private ISeriesService SeriesService { get; set; }

        [Inject] private NavigationManager Navigation { get; set; }

        [Inject] private IModalService Modal { get; set; }

        private List<SeriesListDto> SeriesList { get; set; }
        private bool IsLoading { get; set; }

        private List<TableHeaderData> Header { get; set; } = new List<TableHeaderData>
        {
            new TableHeaderData
                {PropertyName = "Title", DisplayName = "Title", IsSortable = false, Displaying = (e) => (string) e},
            new TableHeaderData
            {
                PropertyName = "StartYear", DisplayName = "StartYear", IsSortable = false,
                Displaying = (e) => WriteHelper.WriteNullableField((int?) e)
            },
            new TableHeaderData
                {PropertyName = "Creator", DisplayName = "Creator", IsSortable = false, Displaying = (e) => (string) e}
        };

        private List<string> Footer { get; } = new List<string> {" ", " ", " "};

        protected override async Task OnInitializedAsync()
        {
            await GetSeries();
        }

        private async Task GetSeries()
        {
            IsLoading = true;
            StateHasChanged();
            SeriesList = await SeriesService.GetAll();
            IsLoading = false;
            Footer[0] = SeriesList.Count().ToString();
            StateHasChanged();
        }

        private void RedirectToData(SeriesListDto series)
        {
            Navigation.NavigateTo($"/series/{series.Id}");
        }

        private void OpenAddSeriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = {ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true}
            };

            Modal.OnClose += AddSeriesDialogClosed;

            Modal.Show<SeriesDialog>("Create Series", parameters, options);
        }

        private async void AddSeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool) modalResult.Data) await GetSeries();

            Modal.OnClose -= AddSeriesDialogClosed;
        }
    }
}