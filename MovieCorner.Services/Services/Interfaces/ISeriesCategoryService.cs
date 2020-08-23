using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesCategoryService: IRepository<SeriesCategory>
    {
        List<SeriesCategorySelectorListDto> GetSelectorList(int seriesId);
    }
}