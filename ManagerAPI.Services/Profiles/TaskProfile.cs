using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System;
using System.Linq;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Task profile for auto mapper
    /// </summary>
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            this.CreateMap<Task, TaskDto>();
            this.CreateMap<IGrouping<DateTime, Task>, TaskDateDto>()
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.TaskList, opt => opt.MapFrom(src => src.ToList()))
                .ForMember(dest => dest.OutOfRange,
                    opt => opt.MapFrom(src =>
                        src.Key < DateTime.Now && src.Count(x => !x.IsSolved) != 0))
                .ForMember(dest => dest.AllSolved,
                    opt => opt.MapFrom(src => src.ToList().Where(x => !x.IsSolved).Count() == 0));
            this.CreateMap<TaskModel, Task>();
            this.CreateMap<Task, TaskListDto>();
        }
    }
}