using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Harkh_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;
    private readonly DatabaseContext _databaseContext;
    private readonly IMilestoneRepository _milestoneRepository;

    public ProjectRepository(DatabaseContext databaseContext, IMilestoneRepository milestoneRepository)
    {
        _databaseContext = databaseContext;
        _projects = databaseContext.Projects;
        _milestoneRepository = milestoneRepository;
    }

    public Project CreateOne(Project newProject)
    {
        _projects.Add(newProject);
        _databaseContext.SaveChanges();
        return newProject;
    }

    public Project? DeleteOne(Guid id)
    {
        Project? FindProject = FindOne(id);
        if (FindProject == null) return null;
        _projects.Remove(FindProject);
        _databaseContext.SaveChanges();
        return FindProject;
    }

    public IEnumerable<Project> FindAll()
    {
        return _projects;
    }

    public Project? FindOne(Guid id)
    {
        return _projects.FirstOrDefault(p => p.Id == id);
    }

    public Project UpdateOne(Project updatedProject)
    {
        _projects.Update(updatedProject);
        _databaseContext.SaveChanges();
        return updatedProject;
    }

    public Project? UpdateProgress(Guid id)
    {
        Project? project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null) return null;
        var milestones = _milestoneRepository.FindAll();
        var milestonesWithProject = milestones.Where(milestone => milestone.ProjectId == id);
        var numberOfMilestones = milestonesWithProject.Count();
        Console.WriteLine($"milestones {numberOfMilestones}");
        double progress = 100;
        double totalProgress = 0;
        double wightOfMilestones = progress / numberOfMilestones;
        Console.WriteLine($"wightOfMilestones: {Math.Round(wightOfMilestones, 2)}");
        foreach (var milestone in milestonesWithProject)
        {
            double milestoneProgress = (double)(milestone.Progress * wightOfMilestones) / 100;
            Console.WriteLine($"progress: {Math.Round(milestoneProgress, 2)}%");
            totalProgress += Math.Round(milestoneProgress, 2);
        }

        Console.WriteLine($"progressOfProject: {totalProgress}%");
        project.Progress = (int)Math.Round(totalProgress);
        project.UpdateAt = DateTime.Now;
        _projects.Update(project);
        _databaseContext.SaveChanges();
        return project;
    }
}
