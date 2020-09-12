using AutoMapper;
using ManagerAPI.Domain.Entities.CSM;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsomorGenerator.Profiles
{
    public class CsomorProfile : Profile
    {
        public CsomorProfile()
        {
            CreateMap<GeneratorSettingsModel, Csomor>();
            CreateMap<PersonModel, CsomorPerson>()
                .ForMember(dest => dest.IgnoredWorks, opt => opt.Ignore());
            CreateMap<WorkModel, CsomorWork>();
            CreateMap<PersonTableModel, CsomorPersonTable>();
            CreateMap<WorkTableModel, CsomorWorkTable>();
            CreateMap<string, IgnoredWork>()
                .ForMember(dest => dest.WorkId, opt => opt.MapFrom(src => src));

            CreateMap<Csomor, GeneratorSettings>()
                .ForMember(dest => dest.Creation, opt => opt.MapFrom(src => (DateTime?)src.Creation))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater == null ? null : src.LastUpdater.UserName))
                .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => (bool?)src.IsShared))
                .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => (bool?)src.IsPublic))
                .ForMember(dest => dest.HasGeneratedCsomor, opt => opt.MapFrom(src => (bool?)src.HasGeneratedCsomor))
                .ForMember(dest => dest.LastGeneration, opt => opt.MapFrom(src => src.LastGeneration))
                .ForMember(dest => dest.SharedWith, opt => opt.MapFrom(src => src.SharedWith));

            CreateMap<UserCsomor, CsomorAccessDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.SharedOn, opt => opt.MapFrom(src => src.SharedOn))
                .ForMember(dest => dest.HasWriteAccess, opt => opt.MapFrom(src => src.HasWriteAccess));

            CreateMap<CsomorPerson, Person>();
            /*CreateMap<IgnoredWork, string>()
                .ForMember(dest => dest, opt => opt.MapFrom(src => src.WorkId));*/
            CreateMap<CsomorWork, Work>();
            CreateMap<CsomorPersonTable, PersonTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)src.Id));
            CreateMap<CsomorWorkTable, WorkTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)src.Id));
        }
    }
}
