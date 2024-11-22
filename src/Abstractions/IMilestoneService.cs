using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IMilestoneService
{
    public Task<IEnumerable<MilestoneReadDto>> FindAll();
    public Task<MilestoneReadDto?> FindOne(Guid id);
    public Task<MilestoneReadDto> CreateOne(MilestoneCreateDto newMilestone);
    public Task<bool> DeleteOne(Guid id);
    public Task<MilestoneReadDto?> UpdateOne(Guid id, MilestoneUpdateDto updatedMilestone);
    // public MilestoneReadDto UpdateStatus(Guid id, MilestoneUpdateStatusDto updatedStatus);
    // public MilestoneReadDto? UpdateProgress(Guid id);
}
