using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

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
        void UpdateImage(int id, SeriesImageModel model);
        void UpdateCategories(int id, SeriesCategoryUpdateModel model);
        void UpdateRate(int id, SeriesRateModel model);
    }
}
