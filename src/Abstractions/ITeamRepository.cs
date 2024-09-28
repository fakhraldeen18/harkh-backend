using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface ITeamRepository
{

    public IEnumerable<Team> FindAll();
    public Team? FindOne(Guid id);
    public Team CreateOne(Team newTeam);
    public Team UpdateOne(Team updateTeam);
    public Team? DeleteOne(Guid id);
}
