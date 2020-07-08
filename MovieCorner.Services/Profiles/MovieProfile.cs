using AutoMapper;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Entities.MC;

namespace MovieCorner.Services.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieListDto>()
                .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.UserName));
            CreateMap<Movie, MovieDto>();
            CreateMap<UserMovie, MovieDto>()
                .ForMember(dest => dest.Seen, opt =>  opt.MapFrom(src => src.Seen))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Movie.Description))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Movie.Year))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Movie.Id));
            CreateMap<MovieCreateDto, Movie>();
        }
    }
}
