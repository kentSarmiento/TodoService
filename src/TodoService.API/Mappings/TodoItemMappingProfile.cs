﻿using AutoMapper;
using TodoService.API.DTOs;
using TodoService.Domain.Entities;

namespace TodoService.API.Mappings
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<CreateTodoItemDTO, TodoItem>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, GetTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, UpdateTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
        }
    }
}
