using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ASPNetTodoService.API.DTOs;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.API.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
            CreateMap<CreateTodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
            CreateMap<TodoItem, GetTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
            CreateMap<TodoItem, UpdateTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
        }
    }
}
