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
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<CreateTodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, GetTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            CreateMap<TodoItem, UpdateTodoItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
        }
    }
}
