using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface ITaskService
{
    public IEnumerable<TaskReadDto> FindAll();
    public TaskReadDto? FindOne(Guid id);
    public Task<TaskReadDto?> CreateOne(TaskCreteDto newTask);
    public Task<bool> DeleteOne(Guid id);
    public Task<TaskReadDto?> UpdateOne(Guid id, TaskUpdateDto updatedTask);
    public Task<TaskReadDto?> UpdateStatus(Guid id, TaskUpdateStatusDto updatedStatus);
    public Task<TaskReadDto?> UpdateProgress(Guid id, TaskUpdateProgressDto updatedProgress);
}
