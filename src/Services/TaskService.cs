using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IMapper mapper, IMilestoneRepository milestoneRepository)
    {
        _taskRepository = taskRepository;
        _milestoneRepository = milestoneRepository;
        _mapper = mapper;
    }

    public TaskReadDto? CreateOne(TaskCreteDto newTask)
    {
        Entities.Task ReadTask = _mapper.Map<Entities.Task>(newTask);
        if (ReadTask == null) return null;
        _taskRepository.CreateOne(ReadTask);
        Milestone? milestone = _milestoneRepository.FindOne(ReadTask.MilestoneId);
        if (milestone?.Id == null) return _mapper.Map<TaskReadDto>(ReadTask); ;
        _milestoneRepository.UpdateProgress(milestone.Id);
        return _mapper.Map<TaskReadDto>(ReadTask);
    }

    public bool DeleteOne(Guid id)
    {
        Entities.Task? findTask = _taskRepository.FindOne(id);
        if (findTask == null) return false;
        _taskRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<TaskReadDto> FindAll()
    {
        IEnumerable<Entities.Task> Tasks = _taskRepository.FindAll();
        IEnumerable<TaskReadDto> readTasks = _mapper.Map<IEnumerable<TaskReadDto>>(Tasks);
        return readTasks;
    }

    public TaskReadDto? FindOne(Guid id)
    {
        Entities.Task? findTask = _taskRepository.FindOne(id);
        if (findTask == null) return null;
        return _mapper.Map<TaskReadDto>(findTask); ;
    }

    public TaskReadDto? UpdateOne(Guid id, TaskUpdateDto updatedTask)
    {
        Entities.Task? Task = _taskRepository.FindOne(id);
        if (Task == null) return null;
        Task.Title = updatedTask.Title;
        Task.Description = updatedTask.Description;
        Task.Progress = updatedTask.Progress;
        Task.Status = updatedTask.Status;
        Task.Priority = updatedTask.Priority;
        Task.DueDate = updatedTask.DueDate;
        Task.UpdateAt = updatedTask.UpdateAt;
        _taskRepository.UpdateOne(Task);
        Milestone? milestone = _milestoneRepository.FindOne(Task.MilestoneId);
        if (milestone?.Id == null) return _mapper.Map<TaskReadDto>(Task);
        _milestoneRepository.UpdateProgress(milestone.Id);
        return _mapper.Map<TaskReadDto>(Task);
    }

    public TaskReadDto? UpdateStatus(Guid id, TaskUpdateStatusDto updatedStatus)
    {
        Entities.Task? Task = _taskRepository.FindOne(id);
        if (Task == null) return null;
        Task.Status = updatedStatus.Status;
        _taskRepository.UpdateOne(Task);
        return _mapper.Map<TaskReadDto>(Task);
    }
    public TaskReadDto? UpdateProgress(Guid id, TaskUpdateProgressDto updatedProgress)
    {
        Entities.Task? Task = _taskRepository.FindOne(id);
        if (Task == null) return null;
        Task.Progress = updatedProgress.Progress;
        _taskRepository.UpdateOne(Task);
        Milestone? milestone = _milestoneRepository.FindOne(Task.MilestoneId);
        if (milestone?.Id == null) return _mapper.Map<TaskReadDto>(Task);
        _milestoneRepository.UpdateProgress(milestone.Id);
        return _mapper.Map<TaskReadDto>(Task);
    }
}
