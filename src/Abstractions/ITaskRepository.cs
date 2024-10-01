namespace Harkh_backend.src.Abstractions;

public interface ITaskRepository
{
    public IEnumerable<Entities.Task> FindAll();
    public Entities.Task? FindOne(Guid id);
    public Entities.Task CreateOne(Entities.Task newTask);
    public Entities.Task? DeleteOne(Guid id);
    public Entities.Task UpdateOne(Entities.Task updatedTask);
    public Guid? FindMilestoneId(Guid id);
}
