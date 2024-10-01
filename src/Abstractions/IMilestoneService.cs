using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IMilestoneService
{
    public IEnumerable<MilestoneReadDto> FindAll();
    public MilestoneReadDto? FindOne(Guid id);
    public MilestoneReadDto CreateOne(MilestoneCreateDto newMilestone);
    public bool DeleteOne(Guid id);
    public MilestoneReadDto? UpdateOne(Guid id, MilestoneUpdateDto updatedMilestone);
   // public MilestoneReadDto UpdateStatus(Guid id, MilestoneUpdateStatusDto updatedStatus);
   // public MilestoneReadDto? UpdateProgress(Guid id);
}
