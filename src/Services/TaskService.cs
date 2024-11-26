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
        Entities.Task readTask = _mapper.Map<Entities.Task>(newTask);
        if (readTask == null) return null;
        if (readTask.Status.ToString() == "Done") readTask.Progress = 100;
        await _unitOfWork.BeginTransaction();
        try
        {
            await _taskRepository.CreateOne(readTask);
            await _unitOfWork.Complete();
            if (readTask.MilestoneId == null)
            {
                await _unitOfWork.CommitTransaction();
                return _mapper.Map<TaskReadDto>(readTask);
            }

            Milestone? milestone = await _milestoneRepository.FindOne(readTask.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(readTask);
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
        IEnumerable<Entities.Task> tasks = await _taskRepository.FindAll();
        IEnumerable<TaskReadDto> readTasks = _mapper.Map<IEnumerable<TaskReadDto>>(tasks);
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
        Entities.Task? task = await _taskRepository.FindOne(id);
        if (task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;
            task.Priority = updatedTask.Priority;
            task.DueDate = updatedTask.DueDate;
            task.UpdateAt = updatedTask.UpdateAt;

            if (task.Status.ToString() == "Done") task.Progress = 100;

            _taskRepository.UpdateOne(task);
            await _unitOfWork.Complete();
            if (task.MilestoneId == null)
            {
                await _unitOfWork.CommitTransaction();
                return _mapper.Map<TaskReadDto>(task);
            }
            Milestone? milestone = await _milestoneRepository.FindOne(task.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<TaskReadDto?> UpdateStatus(Guid id, TaskUpdateStatusDto updatedStatus)
    {
        Entities.Task? task = await _taskRepository.FindOne(id);
        if (task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            task.Status = updatedStatus.Status;
            if (task.Status.ToString() == "Done") task.Progress = 100;
            _taskRepository.UpdateOne(task);
            await _unitOfWork.Complete();
            if (task.MilestoneId == null)
            {
                await _unitOfWork.CommitTransaction();
                return _mapper.Map<TaskReadDto>(task);
            }

            Milestone? milestone = await _milestoneRepository.FindOne(task.MilestoneId);
            await _milestoneRepository.UpdateProgress(milestone?.Id);
            await _unitOfWork.Complete();
            Project? project = await _projectRepository.FindOne(milestone!.ProjectId);
            await _projectRepository.UpdateProgress(project!.Id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }
    public async Task<TaskReadDto?> UpdatePriority(Guid id, TaskUpdatePriorityDto updatedProgress)
    {
        Entities.Task? task = await _taskRepository.FindOne(id);
        if (task == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            task.Priority = updatedProgress.Priority;
            _taskRepository.UpdateOne(task);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<TaskReadDto>(task);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }
}
