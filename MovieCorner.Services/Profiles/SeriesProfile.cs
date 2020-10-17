using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Linq;

namespace MovieCorner.Services.Profiles
{
    /// <summary>
    /// Series profile for auto mapper
    /// </summary>
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            this.CreateMap<EpisodeModel, Episode>();
            this.CreateMap<SeasonModel, Season>();
            this.CreateMap<SeriesModel, Series>();
            this.CreateMap<Series, SeriesListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            this.CreateMap<UserSeries, MySeriesListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Series.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Series.Title))
                .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.Series.StartYear))
                .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => src.Series.EndYear))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Series.Creator.UserName))
                .ForMember(dest => dest.IsAdded, opt => opt.Ignore())
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            this.CreateMap<Series, MySeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.AddedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)));
            this.CreateMap<Season, MySeasonDto>();
            this.CreateMap<Episode, MyEpisodeListDto>()
                .ForMember(dest => dest.Seen, opt => opt.Ignore());
            this.CreateMap<Series, SeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)));
            this.CreateMap<Season, SeasonListDto>();
            this.CreateMap<Episode, EpisodeListDto>();
            this.CreateMap<Series, MySeriesSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            this.CreateMap<Episode, MyEpisodeDto>()
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore())
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.SeenOn, opt => opt.Ignore());
            this.CreateMap<EpisodeShortModel, Episode>();
            this.CreateMap<Episode, EpisodeDto>();
            this.CreateMap<SeriesImageModel, Series>();
            this.CreateMap<SeriesCategory, SeriesCategoryDto>();
            this.CreateMap<SeriesCategoryModel, SeriesCategory>();
            this.CreateMap<SeriesCategory, SeriesCategorySelectorListDto>()
                .ForMember(dest => dest.IsSelected, opt => opt.Ignore());
            this.CreateMap<SeriesComment, SeriesCommentDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
            this.CreateMap<SeriesComment, SeriesCommentListDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.OwnerIsCurrent, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
            this.CreateMap<SeriesCommentModel, SeriesComment>();
            this.CreateMap<EpisodeImageModel, Episode>();
        }
    }
}