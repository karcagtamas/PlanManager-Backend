using AutoMapper;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace MovieCorner.Services.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName));
            CreateMap<UserBook, MyBookListDto>()
                .ForMember(dest => dest.Read, opt =>  opt.MapFrom(src => src.Read))
                .ForMember(dest => dest.Publish, opt => opt.MapFrom(src => src.Book.Publish))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Book.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Book.Creator.UserName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Book.Id));
            CreateMap<Book, MyBookDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
            CreateMap<BookModel, Book>();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
            CreateMap<Book, MyBookSelectorListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.IsMine, opt => opt.Ignore());
        }
    }
}