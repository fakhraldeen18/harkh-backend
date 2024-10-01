using Microsoft.EntityFrameworkCore;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;

namespace Harkh_backend.src.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DbSet<Entities.Task> _Tasks;
    private readonly DatabaseContext _databaseContext;

    public TaskRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _Tasks = databaseContext.Tasks;
    }

    public Entities.Task CreateOne(Entities.Task newTask)
    {
        _Tasks.Add(newTask);
        _databaseContext.SaveChanges();
        return newTask;
    }

    public Entities.Task? DeleteOne(Guid id)
    {
        var FindTask = FindOne(id);
        if (FindTask == null) return null;
        _Tasks.Remove(FindTask);
        _databaseContext.SaveChanges();
        return FindTask;
    }

    public IEnumerable<Entities.Task> FindAll()
    {
        IEnumerable<Entities.Task> Tasks = _Tasks;
        return Tasks;
    }

    public Entities.Task? FindOne(Guid id)
    {
        IEnumerable<Entities.Task> Tasks = _Tasks;
        Entities.Task? findOne = Tasks.FirstOrDefault(t => t.Id == id);
        return findOne;
    }
    public Guid? FindMilestoneId(Guid id)
    {
        IEnumerable<Entities.Task> Tasks = _Tasks;
        Entities.Task? findOne = Tasks.FirstOrDefault(t => t.Id == id);
        if (findOne?.MilestoneId == null) return null;
        return findOne.MilestoneId;
    }
    public Entities.Task UpdateOne(Entities.Task updatedTask)
    {
        _Tasks.Update(updatedTask);
        _databaseContext.SaveChanges();
        return updatedTask;
    }
}
