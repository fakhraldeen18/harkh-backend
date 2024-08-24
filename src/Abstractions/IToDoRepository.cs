using Todo_backend.src.Entities;

namespace Todo_backend.src.Abstractions;

public interface IToDoRepository
{
    public IEnumerable<ToDo> FindAll();
    public ToDo? FindOne(Guid id);
    public ToDo CreateOne(ToDo newToDo);
    public ToDo? DeleteOne(Guid id);
    public ToDo UpdateOne(ToDo updatedToDo);
}
