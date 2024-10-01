using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Harkh_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;

public class MilestoneRepository : IMilestoneRepository
{

    private readonly DbSet<Milestone> _milestones;
    private readonly DatabaseContext _databaseContext;
    private readonly ITaskRepository _taskRepository;

    public MilestoneRepository(DatabaseContext databaseContext, ITaskRepository taskRepository)
    {
        _databaseContext = databaseContext;
        _milestones = databaseContext.Milestones;
        _taskRepository = taskRepository;
    }

    public Milestone CreateOne(Milestone newMilestone)
    {
        _milestones.Add(newMilestone);
        _databaseContext.SaveChanges();
        return newMilestone;
    }

    public Milestone? DeleteOne(Guid id)
    {
        Milestone? FindMilestone = _milestones.FirstOrDefault(m => m.Id == id);
        if (FindMilestone == null) return null;
        _milestones.Remove(FindMilestone);
        _databaseContext.SaveChanges();
        return FindMilestone;
    }

    public IEnumerable<Milestone> FindAll()
    {
        return _milestones;
    }

    public Milestone? FindOne(Guid? id)
    {
        return _milestones.FirstOrDefault(m => m.Id == id);
    }

    public Milestone UpdateOne(Milestone updatedMilestone)
    {
        _milestones.Update(updatedMilestone);
        _databaseContext.SaveChanges();
        return updatedMilestone;
    }
    public Milestone? UpdateProgress(Guid id)
    {
        Milestone? milestone = _milestones.FirstOrDefault(m => m.Id == id);
        if (milestone == null) return null;
        var tasks = _taskRepository.FindAll();
        var tasksWithMilestone = tasks.Where(task => task.MilestoneId == id);
        var numberOfTasks = tasks.Where(task => task.MilestoneId == id).Count();
        Console.WriteLine($"tasks {numberOfTasks}");
        double progress = 100;
        double totalProgress = 0;
        double wightOfTasks = progress / numberOfTasks;
        Console.WriteLine($"wightOfTasks: {Math.Round(wightOfTasks, 2)}");
        foreach (var task in tasksWithMilestone)
        {
            double taskProgress = (double)(task.Progress * wightOfTasks) / 100;
            Console.WriteLine($"progress: {Math.Round(taskProgress, 2)}%");
            totalProgress += Math.Round(taskProgress, 2);
        }

        Console.WriteLine($"progressOfMilestone: {totalProgress}%");
        milestone.Progress = (int)Math.Round(totalProgress);
        _milestones.Update(milestone);
        _databaseContext.SaveChanges();
        return milestone;
    }
}
