using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class UsersSkillsController : CustomController
{
    private readonly IUserSkillService _userSkillService;

    public UsersSkillsController(IUserSkillService userSkillService)
    {
        _userSkillService = userSkillService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> FindAll()
    {
        return Ok(await _userSkillService.FindAll());
    }
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserSkill(Guid userId)
    {
        var result = await _userSkillService.GetUserSkills(userId);
        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserSkill>> CreateOne([FromBody] UserSkillCreateDto newUserSkill)
    {
        if (newUserSkill == null) return BadRequest();
        UserSkill? createdUserSkill = await _userSkillService.CreateOne(newUserSkill);
        return CreatedAtAction(nameof(CreateOne), createdUserSkill);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOne(Guid id)
    {
        var result = await _userSkillService.DeleteOne(id);
        if (result == false) return NotFound();
        return NoContent();
    }
}
