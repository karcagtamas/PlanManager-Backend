using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class CsomorShareComponent
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IGeneratorService GeneratorService { get; set; }

        private List<CsomorAccessDTO> SharedList { get; set; } = new List<CsomorAccessDTO>();
        private List<UserShortDto> CorrectList { get; set; } = new List<UserShortDto>();
        private string Name { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.GetSharedList();
        }

        private async Task GetSharedList()
        {
            this.SharedList = await this.GeneratorService.GetSharedPersonList(this.Id);
        }

        private async Task RefreshCorrectPersons(ChangeEventArgs args)
        {
            if (args != null)
            {
                this.Name = (string)args.Value;
            }
            this.CorrectList = await this.GeneratorService.GetCorrectPersonsForSharing(this.Id, this.Name);
        }

        private async Task AddPerson(string id)
        {
            await this.Save(id, "");
        }

        private async void ChangeAccess(string id)
        {
            var e = this.SharedList.First(x => x.Id == id);
            e.HasWriteAccess = !e.HasWriteAccess;
            await this.Save("", "");
        }

        private async void RemovePerson(string id)
        {
            await this.Save("", id);
        }

        private async Task Save(string id, string exceptId)
        {
            var list = this.SharedList.Where(x => x.Id != exceptId).Select(x => new CsomorAccessModel(x)).ToList();
            if (!string.IsNullOrEmpty(id))
            {
                list.Add(new CsomorAccessModel { Id = id, HasWriteAccess = false });
            }
            await this.GeneratorService.Share(this.Id, list);
            this.Name = "";
            await this.RefreshCorrectPersons(null);
            await this.GetSharedList();
            this.StateHasChanged();
        }
    }
}
