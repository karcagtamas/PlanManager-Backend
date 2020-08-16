using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace MovieCorner.Services.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<EpisodeModel, Episode>();
            CreateMap<SeasonModel, Season>();
            CreateMap<SeriesModel, Series>();
            CreateMap<Series, SeriesListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            CreateMap<UserSeries, MySeriesListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Series.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Series.Title))
                .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.Series.StartYear))
                .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => src.Series.EndYear))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Series.Creator.UserName));
            CreateMap<Series, MySeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<Season, MySeasonDto>();
            CreateMap<Episode, MyEpisodeListDto>()
                .ForMember(dest => dest.Seen, opt => opt.Ignore());
            CreateMap<Series, SeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
            CreateMap<Season, SeasonListDto>();
            CreateMap<Episode, EpisodeListDto>();
            CreateMap<Series, MySeriesSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<Episode, MyEpisodeDto>()
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore());
            CreateMap<EpisodeShortModel, Episode>();
            CreateMap<Episode, EpisodeDto>();
        }
    }
}
