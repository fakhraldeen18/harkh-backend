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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<TeamReadDto>> FindAll()
    {
        IEnumerable<TeamReadDto> teams = _teamService.FindAll();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TeamReadDto> FindOne(Guid id)
    {
        TeamReadDto? team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TeamReadDto> CreateOne([FromBody] TeamCreateDto newTeam)
    {
        if (newTeam == null) return BadRequest();
        TeamReadDto? creatTeam = _teamService.CreateOne(newTeam);
        return CreatedAtAction(nameof(CreateOne), creatTeam);
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TeamReadDto> UpdateOne([FromBody] TeamUpdateDto updateTeam, Guid id)
    {
        var team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        TeamReadDto? updatedTeam = _teamService.UpdateOne(id, updateTeam);
        return Accepted(updatedTeam);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(Guid id)
    {
        TeamReadDto? team = _teamService.FindOne(id);
        if (team == null) return NotFound();
        _teamService.DeleteOne(id);
        return NoContent();
    }
}
