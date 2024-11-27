using System.Collections;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class ExperiencesController : CustomController
{
    private readonly IExperienceService _experienceService;

    public ExperiencesController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperienceReadDto>>> FindAll()
    {
        var experiences = await _experienceService.FindAll();
        return Ok(experiences);
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable>> GetWithUser(Guid userId)
    {
        var experiences = await _experienceService.FindWithUser(userId);
        if (experiences == null) return NotFound();
        return Ok(experiences);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperienceReadDto>> CreateOne([FromBody] ExperienceCreateDto newExperience)
    {
        if (newExperience == null) return BadRequest();
        var createdExperience = await _experienceService.CreateOne(newExperience);
        if (createdExperience == null) return BadRequest();
        return CreatedAtAction(nameof(CreateOne), createdExperience);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOne(Guid id)
    {
        var deleteExperience = await _experienceService.DeleteOne(id);
        if (deleteExperience == false) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateOne(Guid id, [FromBody] ExperienceUpdateDto updateExperience)
    {
        var update = await _experienceService.UpdateOne(id, updateExperience);
        if (update == null) return NotFound();
        return Accepted(update);
    }
}
