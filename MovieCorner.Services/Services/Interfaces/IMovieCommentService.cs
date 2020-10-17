using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;
using System.Collections.Generic;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IMovieCommentService : IRepository<MovieComment>
    {
        List<MovieCommentListDto> GetList(int movieId);
    }
}