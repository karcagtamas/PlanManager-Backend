using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

namespace EventManager.Client.Services.Interfaces
{
    public interface IBookService : IHttpCall<BookListDto, BookDto, BookModel>
    {
        Task<List<MyBookDto>> GetMyBooks();
        Task<bool> UpdateReadStatus(int id, BookReadStatusModel model);
        Task<bool> UpdateMyBooks(MyBookModel model);
        Task<bool> AddBookToMyBooks(int id);
        Task<bool> RemoveBookFromMyBooks(int id);
    }
}