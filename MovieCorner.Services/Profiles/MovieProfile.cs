using AutoMapper;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Entities.MC;
using ManagerAPI.Models.Models.MC;

namespace MovieCorner.Services.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            CreateMap<UserMovie, MyMovieDto>()
                .ForMember(dest => dest.Seen, opt =>  opt.MapFrom(src => src.Seen))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Movie.Description))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Movie.Year))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Movie.Id));
            CreateMap<MovieModel, Movie>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
        }
    }
}
