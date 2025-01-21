using System.Collections;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;


public class EducationsController : CustomController
{
    private readonly IEducationService _educationService;

    public EducationsController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EducationReadDto>>> FindAll()
    {
        var education = await _educationService.FindAll();
        return Ok(education);
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable>> GetWithUser(Guid userId)
    {
        var educations = await _educationService.FindWithUser(userId);
        if (educations == null) return NotFound();
        return Ok(educations);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EducationReadDto>> CreateOne([FromBody] EducationCreateDto newEducation)
    {
        if (newEducation == null) return BadRequest();
        var CreateEducation = await _educationService.CreateOne(newEducation);
        if (CreateEducation == null) return BadRequest();
        return CreatedAtAction(nameof(CreateOne), CreateEducation);

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOne(Guid id)
    {
        var deleteEducation = await _educationService.DeleteOne(id);
        if (deleteEducation == false) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateOne(Guid id, [FromBody] EducationUpdateDto updateExperience)
    {
        var update = await _educationService.UpdateOne(id, updateExperience);
        if (update == null) return NotFound();
        return Accepted(update);
    }


}
