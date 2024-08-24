using Microsoft.EntityFrameworkCore;
using Todo_backend.src.Abstractions;
using Todo_backend.src.Databases;
using Todo_backend.src.Entities;

namespace Todo_backend.src.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly DbSet<ToDo> _toDos;
    private readonly DatabaseContext _databaseContext;

    public ToDoRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _toDos = databaseContext.ToDos;
    }

    public ToDo CreateOne(ToDo newToDo)
    {
        _toDos.Add(newToDo);
        _databaseContext.SaveChanges();
        return newToDo;
    }

    public ToDo? DeleteOne(Guid id)
    {
        var FindTodo = FindOne(id);
        if (FindTodo == null) return null;
        _toDos.Remove(FindTodo);
        _databaseContext.SaveChanges();
        return FindTodo;
    }

    public IEnumerable<ToDo> FindAll()
    {
        IEnumerable<ToDo> toDos = _toDos;
        return toDos;
    }

    public ToDo? FindOne(Guid id)
    {
        IEnumerable<ToDo> toDos = _toDos;
        ToDo? findOne = toDos.FirstOrDefault(t => t.Id == id);
        return findOne;
    }

    public ToDo UpdateOne(ToDo updatedToDo)
    {
        _toDos.Update(updatedToDo);
        _databaseContext.SaveChanges();
        return updatedToDo;
    }
}
