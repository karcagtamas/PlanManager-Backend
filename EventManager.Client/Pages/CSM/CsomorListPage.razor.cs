using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.CSM
{
    public partial class CsomorListPage
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        private IGeneratorService GeneratorService { get; set; }

        private List<CsomorListDTO> OwnedList { get; set; }
        private List<CsomorListDTO> SharedList { get; set; }
        private List<CsomorListDTO> PublicList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetList();
        }

        private async Task GetList()
        {
            await this.GetOwnedList();
            await this.GetSharedList();
            await this.GetPublicList();
            StateHasChanged();
        }

        private async Task GetPublicList()
        {
            this.PublicList = await this.GeneratorService.GetPublicList();
        }

        private async Task GetSharedList()
        {
            this.SharedList = await this.GeneratorService.GetSharedList();
        }

        private async Task GetOwnedList()
        {
            this.OwnedList = await this.GeneratorService.GetOwnedList();
        }
    }
}