using System.Collections;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IUserProjectService
{

    public Task<IEnumerable<UserProject>> FindAll();
    public Task<IEnumerable?> GetProjectUsers(Guid projectId);
    public Task<IEnumerable?> GetUserProjects(Guid userId);
    public Task<UserProject?> CreateOne(UsersProjectsCreateDto newUserProject);
    public Task<bool> DeleteOne(Guid id);

}
