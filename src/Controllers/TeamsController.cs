using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class TeamsController : CustomController
{

    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TeamReadDto>> FindAll()
    {
        IEnumerable<TeamReadDto> teams = _teamService.FindAll();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public ActionResult<TeamReadDto> FindOne(Guid id)
    {
        TeamReadDto? team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpPost]
    public ActionResult<TeamReadDto> CreateOne([FromBody] TeamCreateDto newTeam)
    {
        if (newTeam == null) return BadRequest();
        TeamReadDto? creatTeam = _teamService.CreateOne(newTeam);
        return CreatedAtAction(nameof(CreateOne), creatTeam);
    }


    [HttpPatch("{id}")]
    public ActionResult<TeamReadDto> UpdateOne([FromBody] TeamUpdateDto updateTeam, Guid id)
    {
        var team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        TeamReadDto? updatedTeam = _teamService.UpdateOne(id, updateTeam);
        return Accepted(updatedTeam);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteOne(Guid id)
    {
        TeamReadDto? team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        _teamService.DeleteOne(id);
        return NoContent();
    }
}
