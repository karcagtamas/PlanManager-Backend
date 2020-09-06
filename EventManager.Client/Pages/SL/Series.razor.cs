using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Enums;
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
        [Inject] private IAuthService Auth { get; set; }

        private List<SeriesListDto> SeriesList { get; set; }
        private bool IsLoading { get; set; }
        private bool CanAdd { get; set; }

        private List<TableHeaderData<SeriesListDto>> Header { get; set; } = new List<TableHeaderData<SeriesListDto>>
        {
            new TableHeaderData<SeriesListDto>("Title", true, Alignment.Left)
                {FooterRunnableData = (list) => list.Count.ToString()},
            new TableHeaderData<SeriesListDto>("StartYear", "Start Year", true,
                (e) => WriteHelper.WriteNullableField((int?) e), Alignment.Right),
            new TableHeaderData<SeriesListDto>("Creator", true, Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            await GetSeries();
            this.CanAdd = await Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
        }

        private async Task GetSeries()
        {
            IsLoading = true;
            StateHasChanged();
            SeriesList = await SeriesService.GetAll("Title");
            IsLoading = false;
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