using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IProjectService
{

    public Task<IEnumerable<ProjectReadDto>> FindAll();
    public Task<ProjectReadDto?> FindOne(Guid id);
    public Task<ProjectReadDto?> CreateOne(ProjectCreateDto newProject);
    public Task<bool> DeleteOne(Guid id);
    public Task<ProjectReadDto?> UpdateOne(Guid id, ProjectUpdateDto updatedProject);
    public Task<ProjectReadDto?> UpdateStatus(Guid id, ProjectUpdateStatusDto updatedProject);
}
