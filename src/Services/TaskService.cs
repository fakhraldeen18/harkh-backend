using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Entities.Task> _taskRepository;
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public TaskService(IMapper mapper, IMilestoneRepository milestoneRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = _unitOfWork.Tasks;
        _milestoneRepository = milestoneRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<TaskReadDto?> CreateOne(TaskCreteDto newTask)
    {
        Entities.Task ReadTask = _mapper.Map<Entities.Task>(newTask);
        if (ReadTask == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            await _taskRepository.CreateOne(ReadTask);
            await _unitOfWork.Complete();
            if (ReadTask.MilestoneId == null) return _mapper.Map<TaskReadDto>(ReadTask); ;
            Milestone? milestone = await _milestoneRepository.FindOne(ReadTask.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(ReadTask);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<bool> DeleteOne(Guid id)
    {
        Entities.Task? findTask = await _taskRepository.FindOne(id);
        if (findTask == null) return false;
        await _unitOfWork.BeginTransaction();
        try
        {
            _taskRepository.DeleteOne(findTask);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return true;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return false;
        }
    }

    public async Task<IEnumerable<TaskReadDto>> FindAll()
    {
        IEnumerable<Entities.Task> Tasks = await _taskRepository.FindAll();
        IEnumerable<TaskReadDto> readTasks = _mapper.Map<IEnumerable<TaskReadDto>>(Tasks);
        return readTasks;
    }

    public async Task<TaskReadDto?> FindOne(Guid id)
    {
        Entities.Task? findTask = await _taskRepository.FindOne(id);
        if (findTask == null) return null;
        return _mapper.Map<TaskReadDto>(findTask); ;
    }

    public async Task<TaskReadDto?> UpdateOne(Guid id, TaskUpdateDto updatedTask)
    {
        Entities.Task? Task = await _taskRepository.FindOne(id);
        if (Task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            Task.Title = updatedTask.Title;
            Task.Description = updatedTask.Description;
            Task.Progress = updatedTask.Progress;
            Task.Status = updatedTask.Status;
            Task.Priority = updatedTask.Priority;
            Task.DueDate = updatedTask.DueDate;
            Task.UpdateAt = updatedTask.UpdateAt;
            _taskRepository.UpdateOne(Task);
            await _unitOfWork.Complete();
            if (Task.MilestoneId == null) return _mapper.Map<TaskReadDto>(Task);
            Milestone? milestone = await _milestoneRepository.FindOne(Task.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(Task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<TaskReadDto?> UpdateStatus(Guid id, TaskUpdateStatusDto updatedStatus)
    {
        Entities.Task? Task = await _taskRepository.FindOne(id);
        if (Task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            Task.Status = updatedStatus.Status;
            _taskRepository.UpdateOne(Task);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(Task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }
    public async Task<TaskReadDto?> UpdateProgress(Guid id, TaskUpdateProgressDto updatedProgress)
    {
        Entities.Task? Task = await _taskRepository.FindOne(id);
        if (Task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            Task.Progress = updatedProgress.Progress;
            _taskRepository.UpdateOne(Task);
            await _unitOfWork.Complete();
            if (Task.MilestoneId == null) return _mapper.Map<TaskReadDto>(Task);
            Milestone? milestone = await _milestoneRepository.FindOne(Task.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(Task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }
}
