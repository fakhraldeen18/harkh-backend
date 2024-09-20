using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using AutoMapper;

namespace Harkh_backend.src.Mappers;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();

        CreateMap<Entities.Task, TaskReadDto>();
        CreateMap<TaskCreteDto, Entities.Task>();

    }
}
