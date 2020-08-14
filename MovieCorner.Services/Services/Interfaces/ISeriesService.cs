using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using System.Collections.Generic;
using ManagerAPI.Services.Common;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesService : IRepository<Series>
    {
        List<MySeriesListDto> GetMyList();
        MySeriesDto GetMy(int id);
        void UpdateMySeries(List<int> ids);
        void UpdateSeenStatus(int id, bool seen);
        void AddSeriesToMySeries(int id);
        void RemoveSeriesFromMySeries(int id);
        List<MySeriesSelectorListDto> GetMySelectorList(bool onlyMine);
    }
}
