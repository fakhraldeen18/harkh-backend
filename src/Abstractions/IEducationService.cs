using System.Collections;
using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IEducationService
{
    public Task<IEnumerable<EducationReadDto>> FindAll();
    public Task<EducationReadDto?> CreateOne(EducationCreateDto newEducation);
    public Task<IEnumerable?> FindWithUser(Guid UserId);
    public Task<bool> DeleteOne(Guid id);
    public Task<EducationReadDto?> UpdateOne(Guid UserId, EducationUpdateDto updateEducation);
}
