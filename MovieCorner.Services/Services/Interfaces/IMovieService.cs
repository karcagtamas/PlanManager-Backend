using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using System.Collections.Generic;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IMovieService : IRepository<Movie>
    {
        List<MyMovieDto> GetMyMovies();
        void UpdateSeenStatus(int id, bool seen);
        void UpdateMyMovies(List<int> ids);
        void AddMovieToMyMovies(int id);
        void RemoveMovieFromMyMovies(int id);
    }
}
