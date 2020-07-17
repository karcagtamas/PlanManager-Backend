using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Entities.MC;
using ManagerAPI.Models.Models.MC;

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
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
            CreateMap<UserSeries, MySeriesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Series.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Series.Title))
                .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.Series.StartYear))
                .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => src.Series.EndYear))
                .ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.Series.Seasons))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Series.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.Series.LastUpdater.UserName));
            CreateMap<Season, MySeasonDto>();
            CreateMap<Episode, MyEpisodeDto>()
                .ForMember(dest => dest.Seen, opt => opt.Ignore());
            CreateMap<Series, SeriesDto>();
            CreateMap<Season, SeasonListDto>();
            CreateMap<Episode, EpisodeListDto>();
        }
    }
}
