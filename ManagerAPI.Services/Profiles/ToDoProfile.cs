using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;

namespace ManagerAPI.Services.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo, ToDoDto>();
            CreateMap<ToDoCreateDto, ToDo>();
            CreateMap<ToDoDataDto, ToDo>().ReverseMap();
        }
    }
}
