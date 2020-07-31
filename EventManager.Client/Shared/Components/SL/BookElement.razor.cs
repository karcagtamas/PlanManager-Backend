using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class BookElement
    {
        [Parameter]
        public BookListDto Book { get; set; }

        [Inject]
        private IBookService BookService { get; set; }
    }
}