using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface ITeamService
{
    public IEnumerable<TeamReadDto> FindAll();
    public TeamReadDto? FindOne(Guid id);
    public TeamReadDto? CreateOne(TeamCreateDto newTeam);
    public bool DeleteOne(Guid id);
    public TeamReadDto? UpdateOne(Guid id, TeamUpdateDto updatedTeam);

}
