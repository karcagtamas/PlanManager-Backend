using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("episode");


            ((ModalService) ModalService).OnConfirm += OnConfirm;

            Model = new EpisodeShortModel
            {
                Description = ""
            };

            Context = new EditContext(Model);

            if (Id != 0)
            {
                Episode = await EpisodeService.Get(Id);
                Model = new EpisodeShortModel(Episode);
                IsEdit = true;
                Context = new EditContext(Model);
            }
        }

        private async void OnConfirm()
        {
            var isValid = Context.Validate();
            if (IsEdit)
            {
                if (isValid && await EpisodeService.UpdateShort(this.Id, this.Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}