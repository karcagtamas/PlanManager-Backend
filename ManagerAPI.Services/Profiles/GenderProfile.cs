using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Profiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderDto>();
            CreateMap<Gender, GenderListDto>();
            CreateMap<GenderModel, Gender>();
        }
    }
}