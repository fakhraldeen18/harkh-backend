using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Harkh_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;
public class TeamRepository : ITeamRepository
{

    private readonly DbSet<Team> teams;
    private readonly DatabaseContext _databaseContext;

    public TeamRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        teams = databaseContext.Teams;
    }

    public Team CreateOne(Team newTeam)
    {
        teams.Add(newTeam);
        _databaseContext.SaveChanges();
        return newTeam;
    }

    public Team? DeleteOne(Guid id)
    {
        Team? Team = teams.FirstOrDefault(t => t.Id == id);
        if (Team == null) return null;
        teams.Remove(Team);
        _databaseContext.SaveChanges();
        return Team;
    }

    public IEnumerable<Team> FindAll()
    {
        return teams;
    }

    public Team? FindOne(Guid id)
    {
        return teams.FirstOrDefault(t => t.Id == id);
    }

    public Team UpdateOne(Team updateTeam)
    {
        teams.Update(updateTeam);
        _databaseContext.SaveChanges();
        return updateTeam;
    }
}