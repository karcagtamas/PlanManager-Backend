using System.Collections.Generic;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IBookService : IRepository<Book>
    {
        List<MyBookListDto> GetMyList();
        MyBookDto GetMy(int id);
        void UpdateReadStatus(int id, bool status);
        void UpdateMyBooks(List<int> ids);
        void AddBookToMyBooks(int id);
        void RemoveBookFromMyBooks(int id);
        List<MyBookSelectorListDto> GetMySelectorList();
    }
}
