using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class SkillsController : CustomController
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SkillReadDto>>> FindAll()
    {
        return Ok(await _skillService.FindAll());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SkillReadDto>> CreateOne([FromBody] SkillCreateDto newSkill)
    {
        if (newSkill == null) return BadRequest();
        var createdSkill = await _skillService.CreateOne(newSkill);
        return CreatedAtAction(nameof(CreateOne), createdSkill);

    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillReadDto>> UpdateOne(Guid id, [FromBody] SkillUpdateDto updatedSkill)
    {
        var skills = await _skillService.FindAll();
        var findSkill = skills.FirstOrDefault(s => s.Id == id);
        if (findSkill == null) return NotFound();
        SkillReadDto? skill = await _skillService.UpdateOne(id, updatedSkill);
        return Accepted(skill);
    }
}
