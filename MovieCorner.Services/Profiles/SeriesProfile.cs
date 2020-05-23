using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;

namespace MovieCorner.Services.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series, SeriesListDto>()
                .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater.FullName));
            CreateMap<SeriesDto, Series>();
            CreateMap<Season, SeasonListDto>();
            CreateMap<Episode, EpisodeListDto>();
            CreateMap<EpisodeDto, Episode>();
            CreateMap<IEnumerable<Season>, List<SeriesListDto>>();
        }
    }
}
