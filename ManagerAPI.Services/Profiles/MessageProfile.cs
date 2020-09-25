using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Message profile for auto mapper
    /// </summary>
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(x => x.Sender.UserName));
            CreateMap<Message, MessageListDto>()
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(x => x.Sender.UserName));
            CreateMap<MessageModel, Message>();
        }
    }
}