using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class MovieDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private IMovieService MovieService { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public MovieModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public MovieDto Movie { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("movie");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new MovieModel
            {
                Title = "",
                Description = "",
                ReleaseYear = DateTime.Now.Year
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Movie = await this.MovieService.Get(this.Id);
                this.Model = new MovieModel(this.Movie);
                this.IsEdit = true;
                this.Context = new EditContext(this.Model);
            }
        }

        private async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await this.MovieService.Update(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
            else
            {
                if (isValid && await this.MovieService.Create(this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }
    }
}