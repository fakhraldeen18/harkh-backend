using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IExperienceService
{

    public IEnumerable<ExperienceReadDto> FindAll();

}
