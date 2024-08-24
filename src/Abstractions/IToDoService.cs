using Todo_backend.src.DTOs;
using Todo_backend.src.Entities;

namespace Todo_backend.src.Abstractions;

public interface IToDoService
{
    public IEnumerable<ToDoReadDto> FindAll();
    public ToDoReadDto? FindOne(Guid id);
    public ToDoReadDto? CreateOne(ToDoCreteDto newToDo);
    public bool DeleteOne(Guid id);
    public ToDoReadDto? UpdateOne(Guid id, ToDoUpdateDto updatedToDo);
    public ToDoReadDto? UpdateStatus(Guid id, ToDoUpdateStatusDto updatedStatus);
}
