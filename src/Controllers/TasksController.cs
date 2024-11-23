using Microsoft.AspNetCore.Mvc;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Controllers;

public class TasksController : CustomController
{
    private readonly ITaskService _TaskService;
    private readonly IDocumentService _documentService;

    public TasksController(ITaskService TaskService, IDocumentService documentService)
    {
        _TaskService = TaskService;
        _documentService = documentService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> FindAll()
    {
        IEnumerable<TaskReadDto>? Tasks = await _TaskService.FindAll();
        return Ok(Tasks);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskReadDto>> FindOne(Guid Id)
    {
        var findTask = await _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        return Ok(findTask);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskReadDto>> CreateOne([FromBody] TaskCreteDto newTask)
    {
        if (newTask == null) return BadRequest();
        TaskReadDto? createdTask = await _TaskService.CreateOne(newTask);
        return CreatedAtAction(nameof(CreateOne), createdTask);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOne(Guid Id)
    {
        var findTask = await _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        await _TaskService.DeleteOne(Id);
        return NoContent();
    }

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskReadDto>> UpdateOne(Guid Id, [FromBody] TaskUpdateDto updateTask)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = await _TaskService.UpdateOne(Id, updateTask);
        return Accepted(updatedTask);
    }

    [HttpPatch("UpdateStatus/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskReadDto>> UpdateStatus(Guid Id, [FromBody] TaskUpdateStatusDto updateStatus)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = await _TaskService.UpdateStatus(Id, updateStatus);
        return Accepted(updatedTask);
    }
    [HttpPatch("UpdateProgress/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskReadDto>> UpdateProgress(Guid Id, [FromBody] TaskUpdateProgressDto updateProgress)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = await _TaskService.UpdateProgress(Id, updateProgress);
        return Accepted(updatedTask);
    }
    [HttpPost("CreteDocument")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentReadDto>> CreteDocument([FromBody] DocumentCreateDto newDocument)
    {
        if (newDocument == null) return BadRequest();
        DocumentReadDto? createdDocument = await _documentService.CreateOne(newDocument);
        return CreatedAtAction(nameof(CreteDocument), createdDocument);
    }
}
