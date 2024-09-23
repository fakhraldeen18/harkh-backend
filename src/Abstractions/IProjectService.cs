using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IProjectService
{

    public IEnumerable<ProjectReadDto> FindAll();
    public ProjectReadDto? FindOne(Guid id);
    public ProjectReadDto? CreateOne(ProjectCreatDto newProject);
    public bool DeleteOne(Guid id);
    public ProjectReadDto? UpdateOne(Guid id, ProjectUpdateDto updatedProject);
    public ProjectReadDto? UpdateStatus(Guid id, ProjectUpdateStatusDto updatedProject);

}
