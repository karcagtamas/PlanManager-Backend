using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace MovieCorner.Services.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            CreateMap<UserMovie, MyMovieListDto>()
                .ForMember(dest => dest.IsSeen, opt => opt.MapFrom(src => src.Seen))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Movie.ReleaseYear))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Movie.Creator.UserName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Movie.Id));
            CreateMap<Movie, MyMovieDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<MovieModel, Movie>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.NumberOfSeen,
                    opt => opt.MapFrom(src => this.GetNumberOfSeen(src.ConnectedUsers.ToList())));
            CreateMap<Movie, MyMovieSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
        }

        private int GetNumberOfSeen(List<UserMovie> list)
        {
            return list.Count(x => x.Seen);
        }
    }
}