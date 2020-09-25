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
    public partial class MovieSeenSelectorDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IMovieService MovieService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private List<MyMovieSelectorListDto> List { get; set; }
        private List<int> SelectedIndexList { get; set; } = new List<int>();
        private bool IsLoading { get; set; } = false;
        private List<MovieSeenUpdateModel> SaveList = new List<MovieSeenUpdateModel>();

        private List<TableHeaderData<MyMovieSelectorListDto>> Header { get; set; } = new List<TableHeaderData<MyMovieSelectorListDto>>
        {
            new TableHeaderData<MyMovieSelectorListDto>("Title", Alignment.Left),
            new TableHeaderData<MyMovieSelectorListDto>("ReleaseYear", "Release Year", (e) => ((int) e).ToString(), Alignment.Right),
            new TableHeaderData<MyMovieSelectorListDto>("Creator", Alignment.Left)
        };

        protected override async Task OnInitializedAsync()
        {
            this.FormId = Parameters.Get<int>("FormId");

            await this.GetSelectorList();
            this.SelectedIndexList = this.List.Where(x => x.IsSeen).Select(x => x.Id).ToList();

            ((ModalService)ModalService).OnConfirm += OnConfirm;
        }

        private async Task GetSelectorList()
        {
            this.IsLoading = true;
            StateHasChanged();
            this.List = await this.MovieService.GetMySelectorList(true);
            this.IsLoading = false;
            StateHasChanged();
        }

        private async void OnConfirm()
        {
            if (await this.MovieService.UpdateSeenStatuses(this.SaveList))
            {
                ModalService.Close(ModalResult.Ok(true));
                ((ModalService)ModalService).OnConfirm -= OnConfirm;
            }
        }

        private void SwitchSeenFlag(MyMovieSelectorListDto movie)
        {
            movie.IsSeen = !movie.IsSeen;
            this.SaveList.Add(new MovieSeenUpdateModel { Id = movie.Id, Seen = movie.IsSeen });
            if (movie.IsSeen)
            {
                this.SelectedIndexList.Add(movie.Id);
            }
            else
            {
                this.SelectedIndexList.Remove(movie.Id);
            }
        }
    }
}