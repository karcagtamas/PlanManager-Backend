using System;
using System.Linq;
using AutoMapper;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using ManagerAPI.Domain.Entities;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Task profile for auto mapper
    /// </summary>
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<IGrouping<DateTime, Task>, TaskDateDto>()
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.ToList()))
                .ForMember(dest => dest.OutOfRange, opt => opt.MapFrom(src => (src.Key < DateTime.Now) && (src.ToList().Where(x => !x.IsSolved).Count() != 0)))
                .ForMember(dest => dest.AllSolved, opt => opt.MapFrom(src => src.ToList().Where(x => !x.IsSolved).Count() == 0));
            CreateMap<TaskModel, Task>();
            CreateMap<Task, TaskListDto>();
        }
    }
}
