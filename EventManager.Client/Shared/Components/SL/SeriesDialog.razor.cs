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
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("series");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new SeriesModel
            {
                Title = "",
                Description = "",
                StartYear = null,
                EndYear = null
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Series = await this.SeriesService.Get(this.Id);
                this.Model = new SeriesModel(this.Series);
                this.IsEdit = true;
                this.Context = new EditContext(this.Model);
            }
        }

        private async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await this.SeriesService.Update(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
            else
            {
                if (isValid && await this.SeriesService.Create(this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }
    }
}