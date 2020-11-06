using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class EpisodeDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private IEpisodeService EpisodeService { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public EpisodeShortModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public EpisodeDto Episode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("episode");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new EpisodeShortModel
            {
                Description = ""
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Episode = await this.EpisodeService.Get(this.Id);
                this.Model = new EpisodeShortModel(this.Episode);
                this.IsEdit = true;
                this.Context = new EditContext(this.Model);
            }
        }

        private async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await this.EpisodeService.UpdateShort(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }
    }
}