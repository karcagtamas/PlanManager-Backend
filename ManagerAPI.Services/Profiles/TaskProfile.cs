using System;
using System.Linq;
using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<IGrouping<DateTime, Task>, TaskDateDto>()
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.ToList()));
            CreateMap<TaskModel, Task>();
            CreateMap<Task, TaskDataDto>();
            CreateMap<TaskDataDto, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
