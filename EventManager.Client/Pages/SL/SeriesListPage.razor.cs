using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Components.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.SL
{
    public partial class SeriesListPage
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
            await this.GetSeries();
            this.CanAdd = await this.Auth.HasRole("Administrator", "Status Library Moderator",
                "Status Library Administrator", "Root");
        }

        private async Task GetSeries()
        {
            this.IsLoading = true;
            this.StateHasChanged();
            this.SeriesList = await this.SeriesService.GetAll("Title");
            this.IsLoading = false;
            this.StateHasChanged();
        }

        private void RedirectToData(SeriesListDto series)
        {
            this.Navigation.NavigateTo($"/series/{series.Id}");
        }

        private void OpenAddSeriesDialog()
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);

            var options = new ModalOptions
            {
                ButtonOptions = { ConfirmButtonType = ConfirmButton.Save, ShowConfirmButton = true }
            };

            this.Modal.OnClose += this.AddSeriesDialogClosed;

            this.Modal.Show<SeriesDialog>("Create Series", parameters, options);
        }

        private async void AddSeriesDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data)
            {
                await this.GetSeries();
            }

            this.Modal.OnClose -= this.AddSeriesDialogClosed;
        }
    }
}