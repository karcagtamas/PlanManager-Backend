using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;
using System.Collections.Generic;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesCommentService : IRepository<SeriesComment>
    {
        List<SeriesCommentListDto> GetList(int seriesId);
    }
}