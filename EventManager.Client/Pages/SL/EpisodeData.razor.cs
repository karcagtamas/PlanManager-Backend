using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.SL
{
    public partial class EpisodeData
    {
        [Parameter] public int Id { get; set; }

        private MyEpisodeDto Episode { get; set; }
        [Inject] private IEpisodeService EpisodeService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEpisode();
        }

        private async Task GetEpisode()
        {
            IsLoading = true;
            StateHasChanged();
            this.Episode = await EpisodeService.GetMy(this.Id);
            IsLoading = false;
            StateHasChanged();
        }
    }
}