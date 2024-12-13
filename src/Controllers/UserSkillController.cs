using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class UserSkillController : CustomController
{
    private readonly IUserSkillService _userSkillService;

    public UserSkillController(IUserSkillService userSkillService)
    {
        _userSkillService = userSkillService;
    }

    [HttpGet]
    public async Task<ActionResult> FindAll()
    {
        return Ok(await _userSkillService.FindAll());
    }
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserSkill(Guid userId)
    {
        var result = await _userSkillService.GetUserSkills(userId);
        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<UserSkill>> CreateOne([FromBody] UserSkillCreateDto newUserSkill)
    {
        if (newUserSkill == null) return BadRequest();
        UserSkill? createdUserSkill = await _userSkillService.CreateOne(newUserSkill);
        return CreatedAtAction(nameof(CreateOne), createdUserSkill);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOne(Guid id)
    {
        var result = await _userSkillService.DeleteOne(id);
        if (result == false) return NotFound();
        return NoContent();
    }
}
