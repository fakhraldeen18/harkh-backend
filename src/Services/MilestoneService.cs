using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public MilestoneService(IMilestoneRepository milestoneRepository, ITaskService taskService, IMapper mapper)
    {
        _milestoneRepository = milestoneRepository;
        _taskService = taskService;
        _mapper = mapper;
    }

    public MilestoneReadDto CreateOne(MilestoneCreateDto newMilestone)
    {
        Milestone milestone = _mapper.Map<Milestone>(newMilestone);
        _milestoneRepository.CreateOne(milestone);
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public bool DeleteOne(Guid id)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return false;
        _milestoneRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<MilestoneReadDto> FindAll()
    {
        IEnumerable<Milestone> milestones = _milestoneRepository.FindAll();
        return _mapper.Map<IEnumerable<MilestoneReadDto>>(milestones);
    }

    public MilestoneReadDto? FindOne(Guid id)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public MilestoneReadDto? UpdateOne(Guid id, MilestoneUpdateDto updatedMilestone)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        milestone.ProjectId = updatedMilestone.ProjectId;
        milestone.Name = updatedMilestone.Name;
        milestone.Progress = updatedMilestone.Progress;
        milestone.DueDate = updatedMilestone.DueDate;
        _milestoneRepository.UpdateOne(milestone);
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    // public MilestoneReadDto? UpdateProgress(Guid id)
    // {
    //     Milestone? milestone = _milestoneRepository.FindAll().FirstOrDefault(m => m.Id == id);
    //     if (milestone == null) return null;
    //     var tasks = _taskService.FindAll();
    //     var tasksWithMilestone = tasks.Where(task => task.MilestoneId == id);
    //     var numberOfTasks = tasks.Where(task => task.MilestoneId == id).Count();
    //     Console.WriteLine($"tasks {numberOfTasks}");
    //     double progress = 100;
    //     double totalProgress = 0;
    //     double wightOfTasks = progress / numberOfTasks;
    //     Console.WriteLine($"wightOfTasks: {Math.Round(wightOfTasks, 2)}");
    //     foreach (var task in tasksWithMilestone)
    //     {
    //         double taskProgress = (double)(task.Progress * wightOfTasks) / 100;
    //         Console.WriteLine($"progress: {Math.Round(taskProgress, 2)}%");
    //         totalProgress += Math.Round(taskProgress, 2);
    //     }

    //     Console.WriteLine($"progressOfMilestone: {totalProgress}%");
    //     milestone.Progress = (int)Math.Round(totalProgress);
    //     _milestoneRepository.UpdateOne(milestone);
    //     return _mapper.Map<MilestoneReadDto>(milestone);
    // }
}


// find the number of tasks related with this milestone (x)
// put the progress of milestone = 100 (x)
// find the wight of each task by dividend number of tasks in 100 (x)

// map the task and multiply the wight of each task to his progress value 
