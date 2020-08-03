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
    public partial class BookDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private IBookService BookService { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public int FormId { get; set; }

        public BookModel Model { get; set; }
        public EditContext Context { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public BookDto Book { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("book");


            ((ModalService) ModalService).OnConfirm += OnConfirm;

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
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
            else
            {
                if (isValid && await BookService.Create(Model))
                {
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService) ModalService).OnConfirm -= OnConfirm;
                }
            }
        }
    }
}