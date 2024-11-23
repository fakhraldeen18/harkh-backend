using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;
public interface IProjectRepository
{
    public Task<IEnumerable<Project>> FindAll();
    public Task<Project?> FindOne(Guid id);
    public Task<Project> CreateOne(Project newProject);
    public Project UpdateOne(Project updatedProject);
    public Task<Project?> UpdateProgress(Guid id);
    public Project DeleteOne(Project project);
}
