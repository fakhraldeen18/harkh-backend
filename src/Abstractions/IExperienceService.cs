using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IExperienceService
{

    public Task<IEnumerable<ExperienceReadDto>> FindAll();

}
