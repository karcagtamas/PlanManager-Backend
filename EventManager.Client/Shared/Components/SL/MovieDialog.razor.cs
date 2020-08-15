using System;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("movie");


            ((ModalService) ModalService).OnConfirm += OnConfirm;

            Model = new MovieModel
            {
                Title = "",
                Description = "",
                ReleaseYear = DateTime.Now.Year
            };

            Context = new EditContext(Model);

            if (Id != 0)
            {
                Movie = await MovieService.Get(Id);
                Model = new MovieModel(Movie);
                IsEdit = true;
                Context = new EditContext(Model);
            }
        }

        private async void OnConfirm()
        {
            var isValid = Context.Validate();
            if (IsEdit)
            {
                if (isValid && await MovieService.Update(Id, Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await MovieService.Create(Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}