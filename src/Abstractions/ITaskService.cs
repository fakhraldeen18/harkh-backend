using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface ITaskService
{
    public IEnumerable<TaskReadDto> FindAll();
    public TaskReadDto? FindOne(Guid id);
    public TaskReadDto? CreateOne(TaskCreteDto newTask);
    public bool DeleteOne(Guid id);
    public TaskReadDto? UpdateOne(Guid id, TaskUpdateDto updatedTask);
    public TaskReadDto? UpdateStatus(Guid id, TaskUpdateStatusDto updatedStatus);
    public TaskReadDto? UpdateProgress(Guid id, TaskUpdateProgressDto updatedProgress);
}
