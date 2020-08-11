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
    public partial class SeriesDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private ISeriesService SeriesService { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public SeriesModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public SeriesDto Series { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("series");


            ((ModalService) ModalService).OnConfirm += OnConfirm;

            Model = new SeriesModel
            {
                Title = "",
                Description = "",
                StartYear = null,
                EndYear = null
            };

            Context = new EditContext(Model);

            if (Id != 0)
            {
                Series = await SeriesService.Get(Id);
                Model = new SeriesModel(Series);
                IsEdit = true;
                Context = new EditContext(Model);
            }
        }

        private async void OnConfirm()
        {
            var isValid = Context.Validate();
            if (IsEdit)
            {
                if (isValid && await SeriesService.Update(Id, Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await SeriesService.Create(Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}