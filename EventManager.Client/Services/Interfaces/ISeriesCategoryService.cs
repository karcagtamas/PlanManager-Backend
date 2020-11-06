using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeriesCategoryService : IHttpCall<SeriesCategoryDto, SeriesCategoryDto, SeriesCategoryModel>
    {
        Task<List<SeriesCategorySelectorListDto>> GetSelectorList(int seriesId);
    }
}