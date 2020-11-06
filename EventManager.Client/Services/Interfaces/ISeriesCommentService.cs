using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeriesCommentService : IHttpCall<SeriesCommentListDto, SeriesCommentDto, SeriesCommentModel>
    {
        Task<List<SeriesCommentListDto>> GetList(int seriesId);
    }
}