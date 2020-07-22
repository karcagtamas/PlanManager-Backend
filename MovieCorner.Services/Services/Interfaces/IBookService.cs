using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IBookService : IRepository<Book>
    {
        List<MyBookDto> GetMyList();
        void UpdateReadStatus(int id, bool status);
        void UpdateMyBooks(List<int> ids);
    }
}
