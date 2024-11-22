using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface ISkillService
{
    public IEnumerable<SkillReadDto> FindAll();
    public SkillReadDto? CreateOne(SkillCreateDto newSkill);
    public SkillReadDto? UpdateOne(Guid id,SkillUpdateDto updateSkill);
}
