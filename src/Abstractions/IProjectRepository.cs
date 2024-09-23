using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;
public interface IProjectRepository
{
    public IEnumerable<Project> FindAll();
    public Project? FindOne(Guid id);
    public Project CreateOne(Project newProject);
    public Project UpdateOne(Project updatedProject);
    public Project? DeleteOne(Guid id);
}
