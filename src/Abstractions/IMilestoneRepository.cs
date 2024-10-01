using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IMilestoneRepository
{
    public IEnumerable<Milestone> FindAll();
    public Milestone? FindOne(Guid? id);
    public Milestone CreateOne(Milestone newMilestone);
    public Milestone UpdateOne(Milestone updatedMilestone);
    public Milestone? UpdateProgress(Guid id);
    public Milestone? DeleteOne(Guid id);

}
