using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface ISkillService
{
    public Task<IEnumerable<SkillReadDto>> FindAll();
    public Task<SkillReadDto?> CreateOne(SkillCreateDto newSkill);
    public Task<SkillReadDto?> UpdateOne(Guid id, SkillUpdateDto updateSkill);
}
