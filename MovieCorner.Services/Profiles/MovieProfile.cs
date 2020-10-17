using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Linq;

namespace MovieCorner.Services.Profiles
{
    /// <summary>
    /// Movie profile for auto mapper
    /// </summary>
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            this.CreateMap<Movie, MovieListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            this.CreateMap<UserMovie, MyMovieListDto>()
                .ForMember(dest => dest.IsSeen, opt => opt.MapFrom(src => src.IsSeen))
                .ForMember(dest => dest.IsAdded, opt => opt.MapFrom(src => src.IsAdded))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Movie.ReleaseYear))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Movie.Creator.UserName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Movie.Id));
            this.CreateMap<Movie, MyMovieDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)))
                .ForMember(dest => dest.NumberOfSeen,
                    opt => opt.MapFrom(src => this.GetNumberOfSeen(src.ConnectedUsers.ToList())))
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore())
                .ForMember(dest => dest.AddedOn, opt => opt.Ignore())
                .ForMember(dest => dest.SeenOn, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore());
            this.CreateMap<MovieModel, Movie>();
            this.CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)))
                .ForMember(dest => dest.NumberOfSeen,
                    opt => opt.MapFrom(src => this.GetNumberOfSeen(src.ConnectedUsers.ToList())));
            this.CreateMap<Movie, MyMovieSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            this.CreateMap<MovieImageModel, Movie>();
            this.CreateMap<MovieCategory, MovieCategoryDto>();
            this.CreateMap<MovieCategoryModel, MovieCategory>();
            this.CreateMap<MovieCategory, MovieCategorySelectorListDto>()
                .ForMember(dest => dest.IsSelected, opt => opt.Ignore());
            this.CreateMap<MovieComment, MovieCommentDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
            this.CreateMap<MovieComment, MovieCommentListDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.OwnerIsCurrent, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
            this.CreateMap<MovieCommentModel, MovieComment>();
        }

        /// <summary>
        /// Get number of members whose saw the movie
        /// </summary>
        /// <param name="list">List of maps</param>
        /// <returns>Number of members</returns>
        private int GetNumberOfSeen(List<UserMovie> list)
        {
            return list?.Count(x => x.IsSeen) ?? 0;
        }
    }
}