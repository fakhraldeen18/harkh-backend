using Todo_backend.src.DTOs;
using Todo_backend.src.Entities;
using AutoMapper;

namespace Todo_backend.src.Mappers;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();

        CreateMap<ToDo, ToDoReadDto>();
        CreateMap<ToDoCreteDto, ToDo>();

    }
}
