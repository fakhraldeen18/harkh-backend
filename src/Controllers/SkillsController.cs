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
    public ActionResult<IEnumerable<SkillReadDto>> FindAll()
    {
        return Ok(_skillService.FindAll());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<SkillReadDto> CreateOne([FromBody] SkillCreateDto newSkill)
    {
        if (newSkill == null) return BadRequest();
        var createdSkill = _skillService.CreateOne(newSkill);
        return CreatedAtAction(nameof(CreateOne), createdSkill);

    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SkillReadDto> UpdateOne(Guid id, [FromBody] SkillUpdateDto updatedSkill)
    {
        SkillReadDto? findSkill = _skillService.FindAll().FirstOrDefault(s => s.Id == id);
        if (findSkill == null) return NotFound();
        SkillReadDto? skill = _skillService.UpdateOne(id, updatedSkill);
        return Accepted(skill);
    }
}
