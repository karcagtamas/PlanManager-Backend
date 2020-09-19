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
    public partial class BookDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }
        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }
        [Inject] private IBookService BookService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        private int FormId { get; set; }
        private BookModel Model { get; set; }
        private EditContext Context { get; set; }
        private bool IsEdit { get; set; }
        private int Id { get; set; }
        private BookDto Book { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("book");

            ((ModalService)ModalService).OnConfirm += OnConfirm;

            Model = new BookModel
            {
                Name = "",
                Description = "",
                Author = "",
                Publish = null
            };

            Context = new EditContext(Model);

            if (Id != 0)
            {
                Book = await BookService.Get(Id);
                Model = new BookModel(Book);
                IsEdit = true;
                Context = new EditContext(Model);
            }
        }

        private async void OnConfirm()
        {
            var isValid = Context.Validate();
            if (IsEdit)
            {
                if (isValid && await BookService.Update(Id, Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await BookService.Create(Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}