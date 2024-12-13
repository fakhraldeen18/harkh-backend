using System.Collections;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IUserSkillService
{

    public Task<IEnumerable> FindAll();
    public Task<UserSkill?> CreateOne(UserSkillCreateDto newUserSkill);
    public Task<IEnumerable?> GetUserSkills(Guid userId);
    public Task<bool> DeleteOne(Guid id);

}
