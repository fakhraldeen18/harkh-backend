using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class MilestonesController : CustomController
{
    private readonly IMilestoneService _milestoneService;
    private readonly IDocumentService _documentService;

    public MilestonesController(IMilestoneService milestoneService, IDocumentService documentService)
    {
        _milestoneService = milestoneService;
        _documentService = documentService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<MilestoneReadDto>> FindAll()
    {
        var milestones = _milestoneService.FindAll();
        return Ok(milestones);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MilestoneReadDto> FindOne(Guid id)
    {
        var findMilestone = _milestoneService.FindOne(id);
        if (findMilestone == null) return NotFound();
        return Ok(findMilestone);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MilestoneReadDto> CreateOne([FromBody] MilestoneCreateDto newMilestone)
    {
        if (newMilestone == null) return BadRequest();
        var createMilestone = _milestoneService.CreateOne(newMilestone);
        return CreatedAtAction(nameof(CreateOne), createMilestone);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(Guid id)
    {
        var findMilestone = _milestoneService.FindOne(id);
        if (findMilestone == null) return NotFound();
        _milestoneService.DeleteOne(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MilestoneReadDto> UpdateOne(Guid id, [FromBody] MilestoneUpdateDto updateMilestone)
    {
        var milestone = _milestoneService.FindOne(id);
        if (milestone == null) return NotFound();
        var updatedMilestone = _milestoneService.UpdateOne(id, updateMilestone);
        return Accepted(updatedMilestone);
    }
    [HttpPost("CreteDocument")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DocumentReadDto> CreteDocument([FromBody] DocumentCreateDto newDocument)
    {
        if (newDocument == null) return BadRequest();
        DocumentReadDto? createdDocument = _documentService.CreateOne(newDocument);
        return CreatedAtAction(nameof(CreteDocument), createdDocument);
    }
}
