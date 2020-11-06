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
            this.FormId = this.Parameters.Get<int>("FormId");
            this.Id = this.Parameters.TryGet<int>("book");

            ((ModalService)this.ModalService).OnConfirm += this.OnConfirm;

            this.Model = new BookModel
            {
                Name = "",
                Description = "",
                Author = "",
                Publish = null
            };

            this.Context = new EditContext(this.Model);

            if (this.Id != 0)
            {
                this.Book = await this.BookService.Get(this.Id);
                this.Model = new BookModel(this.Book);
                this.IsEdit = true;
                this.Context = new EditContext(this.Model);
            }
        }

        private async void OnConfirm()
        {
            bool isValid = this.Context.Validate();
            if (this.IsEdit)
            {
                if (isValid && await this.BookService.Update(this.Id, this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
            else
            {
                if (isValid && await this.BookService.Create(this.Model))
                {
                    this.ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)this.ModalService).OnConfirm -= this.OnConfirm;
                }
            }
        }
    }
}