using System.Collections;
using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IExperienceService
{
    public Task<IEnumerable<ExperienceReadDto>> FindAll();
    public Task<ExperienceReadDto?> CreateOne(ExperienceCreateDto newExperience);
    public Task<IEnumerable?> FindWithUser(Guid id);
    public Task<bool> DeleteOne(Guid id);
    public Task<ExperienceReadDto?> UpdateOne(Guid id, ExperienceUpdateDto updateExperience);
}
