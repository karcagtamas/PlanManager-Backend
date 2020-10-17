using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Gender profile for auto mapper
    /// </summary>
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            this.CreateMap<Gender, GenderDto>();
            this.CreateMap<Gender, GenderListDto>();
            this.CreateMap<GenderModel, Gender>();
        }
    }
}