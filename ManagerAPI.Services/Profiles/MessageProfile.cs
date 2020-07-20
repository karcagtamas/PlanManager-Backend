using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Services.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(x => x.Sender.UserName));
        }
    }
}
