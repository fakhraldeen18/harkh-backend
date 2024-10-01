using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class MilestonesController : CustomController
{
    private readonly IMilestoneService _milestoneService;

    public MilestonesController(IMilestoneService milestoneService)
    {
        _milestoneService = milestoneService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MilestoneReadDto>> FindAll()
    {
        var milestones = _milestoneService.FindAll();
        return Ok(milestones);
    }

    [HttpGet("{id}")]
    public ActionResult<MilestoneReadDto> FindOne(Guid id)
    {
        var findMilestone = _milestoneService.FindOne(id);
        if (findMilestone == null) return NotFound();
        return Ok(findMilestone);
    }

    [HttpPost]
    public ActionResult<MilestoneReadDto> CreateOne([FromBody] MilestoneCreateDto newMilestone)
    {
        if (newMilestone == null) return BadRequest();
        var createMilestone = _milestoneService.CreateOne(newMilestone);
        return CreatedAtAction(nameof(CreateOne), createMilestone);
    }

    [HttpDelete("{id}")]

    public ActionResult DeleteOne(Guid id)
    {
        var findMilestone = _milestoneService.FindOne(id);
        if (findMilestone == null) return NotFound();
        _milestoneService.DeleteOne(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult<MilestoneReadDto> UpdateOne(Guid id, [FromBody] MilestoneUpdateDto updateMilestone)
    {
        var milestone = _milestoneService.FindOne(id);
        if (milestone == null) return NotFound();
        var updatedMilestone = _milestoneService.UpdateOne(id, updateMilestone);
        return Accepted(updatedMilestone);
    }

    // [HttpPatch("UpdateProgress/{id}")]
    // public ActionResult<MilestoneReadDto> UpdateProgress([FromBody] MilestoneUpdateProgressDto updateMilestone, Guid id)
    // {
    //     var milestone = _milestoneService.FindOne(id);
    //     if (milestone == null) return NotFound();
    //     var updatedMilestone = _milestoneService.UpdateProgress(id, updateMilestone);
    //     return Accepted(updatedMilestone);
    // }
}
