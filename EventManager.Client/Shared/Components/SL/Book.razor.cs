using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class Book
    {
        [Parameter]
        public BookListDto BookElement { get; set; }

        [Inject]
        private IBookService BookService { get; set; }
    }
}