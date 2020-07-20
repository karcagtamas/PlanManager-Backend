using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookListDto>> GetBooks();
        Task<BookDto> GetBook(int id);
        Task<List<MyBookDto>> GetMyBooks();
        Task<bool> CreateBook(BookModel model);
        Task<bool> UpdateBook(int id, BookModel model);
        Task<bool> DeleteBook(int id);
        Task<bool> UpdateReadStatus(int id, bool status);
        Task<bool> UpdateMyBooks(List<int> ids);
    }
}