using AutoMapper;
using Todo_backend.src.Abstractions;
using Todo_backend.src.DTOs;
using Todo_backend.src.Entities;

namespace Todo_backend.src.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;

    public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
    }

    public ToDoReadDto? CreateOne(ToDoCreteDto newToDo)
    {
        ToDo ReadToDo = _mapper.Map<ToDo>(newToDo);
        if (ReadToDo == null) return null;
        _toDoRepository.CreateOne(ReadToDo);
        return _mapper.Map<ToDoReadDto>(ReadToDo);
    }

    public bool DeleteOne(Guid id)
    {
        ToDo? findToDo = _toDoRepository.FindOne(id);
        if (findToDo == null) return false;
        _toDoRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<ToDoReadDto> FindAll()
    {
        IEnumerable<ToDo> toDos = _toDoRepository.FindAll();
        IEnumerable<ToDoReadDto> readToDos = _mapper.Map<IEnumerable<ToDoReadDto>>(toDos);
        return readToDos;
    }

    public ToDoReadDto? FindOne(Guid id)
    {
        ToDo? findToDo = _toDoRepository.FindOne(id);
        if (findToDo == null) return null;
        return _mapper.Map<ToDoReadDto>(findToDo); ;
    }

    public ToDoReadDto? UpdateOne(Guid id, ToDoUpdateDto updatedToDo)
    {
        ToDo? toDo = _toDoRepository.FindOne(id);
        if (toDo == null) return null;
        toDo.Title = updatedToDo.Title;
        toDo.Description = updatedToDo.Description;
        toDo.Status = updatedToDo.Status;
        toDo.CreatedAt = updatedToDo.CreatedAt;
        toDo.EndDate = updatedToDo.EndDate;
        _toDoRepository.UpdateOne(toDo);
        return _mapper.Map<ToDoReadDto>(toDo);
    }

    public ToDoReadDto? UpdateStatus(Guid id, ToDoUpdateStatusDto updatedStatus)
    {
        ToDo? toDo = _toDoRepository.FindOne(id);
        if (toDo == null) return null;
        toDo.Status = updatedStatus.Status;
        _toDoRepository.UpdateOne(toDo);
        return _mapper.Map<ToDoReadDto>(toDo);
    }
}
