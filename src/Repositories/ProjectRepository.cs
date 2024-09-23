using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Harkh_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;
    private readonly DatabaseContext _databaseContext;

    public ProjectRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _projects = databaseContext.Projects;
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
}
