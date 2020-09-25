﻿using System.Linq;
using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace MovieCorner.Services.Profiles
{
    /// <summary>
    /// Series profile for auto mapper
    /// </summary>
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
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Series.Creator.UserName))
                .ForMember(dest => dest.IsAdded, opt => opt.Ignore())
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<Series, MySeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.AddedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)));
            CreateMap<Season, MySeasonDto>();
            CreateMap<Episode, MyEpisodeListDto>()
                .ForMember(dest => dest.Seen, opt => opt.Ignore());
            CreateMap<Series, SeriesDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Categories.Select(x => x.Category.Name)));
            CreateMap<Season, SeasonListDto>();
            CreateMap<Episode, EpisodeListDto>();
            CreateMap<Series, MySeriesSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<Episode, MyEpisodeDto>()
                .ForMember(dest => dest.IsSeen, opt => opt.Ignore())
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.SeenOn, opt => opt.Ignore());
            CreateMap<EpisodeShortModel, Episode>();
            CreateMap<Episode, EpisodeDto>();
            CreateMap<SeriesImageModel, Series>();
            CreateMap<SeriesCategory, SeriesCategoryDto>();
            CreateMap<SeriesCategoryModel, SeriesCategory>();
            CreateMap<SeriesCategory, SeriesCategorySelectorListDto>()
                .ForMember(dest => dest.IsSelected, opt => opt.Ignore());
            CreateMap<SeriesComment, SeriesCommentDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<SeriesComment, SeriesCommentListDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.OwnerIsCurrent, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
            CreateMap<SeriesCommentModel, SeriesComment>();
            CreateMap<EpisodeImageModel, Episode>();
        }
    }
}