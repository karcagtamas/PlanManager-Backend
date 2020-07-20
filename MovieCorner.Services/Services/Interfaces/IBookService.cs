using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IBookService
    {
        List<BookListDto> GetBooks();
        BookDto GetBook(int id);
        List<MyBookDto> GetMyBooks();
        void CreateBook(BookModel model);
        void UpdateBook(int id, BookModel model);
        void DeleteBook(int id);
        void UpdateReadStatus(int id, bool status);
        void UpdateMyBooks(List<int> ids);
    }
}
