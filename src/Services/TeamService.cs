using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;

    public TeamService(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public TeamReadDto? CreateOne(TeamCreateDto newTeam)
    {
        if (newTeam == null) return null;
        Team createTeam = _mapper.Map<Team>(newTeam);
        _teamRepository.CreateOne(createTeam);
        return _mapper.Map<TeamReadDto>(createTeam);
    }

    public bool DeleteOne(Guid id)
    {
        Team? team = _teamRepository.FindOne(id);
        if (team == null) return false;
        _teamRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<TeamReadDto> FindAll()
    {
        IEnumerable<Team> teams = _teamRepository.FindAll();
        return _mapper.Map<IEnumerable<TeamReadDto>>(teams);
    }

    public TeamReadDto? FindOne(Guid id)
    {
        Team? Team = _teamRepository.FindOne(id);
        if (Team == null) return null;
        return _mapper.Map<TeamReadDto>(Team);
    }

    public TeamReadDto? UpdateOne(Guid id, TeamUpdateDto updatedTeam)
    {
        var team = _teamRepository.FindOne(id);
        if (team == null) return null;
        team.ProjectId = updatedTeam.ProjectId;
        team.Name = updatedTeam.Name;
        team.UpdateAt = updatedTeam.UpdateAt;
        _teamRepository.UpdateOne(team);
        return _mapper.Map<TeamReadDto>(team);
    }
}
