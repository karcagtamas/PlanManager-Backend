using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IBookService : IHttpCall<BookListDto, BookDto, BookModel>
    {
        Task<List<MyBookListDto>> GetMyList();
        Task<MyBookDto> GetMy(int id);
        Task<bool> UpdateReadStatuses(List<BookReadStatusModel> models);
        Task<bool> UpdateMyBooks(MyBookModel model);
        Task<bool> AddBookToMyBooks(int id);
        Task<bool> RemoveBookFromMyBooks(int id);
        Task<List<MyBookSelectorListDto>> GetMySelectorList(bool onlyMine);
    }
}