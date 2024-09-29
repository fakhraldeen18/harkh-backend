using Microsoft.AspNetCore.Mvc;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Controllers;

public class TasksController : CustomController
{
    private readonly ITaskService _TaskService;

    public TasksController(ITaskService TaskService)
    {
        _TaskService = TaskService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<TaskReadDto>> FindAll()
    {
        IEnumerable<TaskReadDto>? Tasks = _TaskService.FindAll();
        return Ok(Tasks);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TaskReadDto> FindOne(Guid Id)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        return Ok(findTask);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TaskReadDto> CreateOne([FromBody] TaskCreteDto newTask)
    {
        if (newTask == null) return BadRequest();
        TaskReadDto? createdTask = _TaskService.CreateOne(newTask);
        return CreatedAtAction(nameof(CreateOne), createdTask);
    }

    [HttpDelete("{Id}")]
    public ActionResult DeleteOne(Guid Id)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        _TaskService.DeleteOne(Id);
        return NoContent();
    }

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TaskReadDto> UpdateOne(Guid Id, [FromBody] TaskUpdateDto updateTask)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = _TaskService.UpdateOne(Id, updateTask);
        return Accepted(updatedTask);
    }

    [HttpPatch("UpdateStatus/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TaskReadDto> UpdateStatus(Guid Id, [FromBody] TaskUpdateStatusDto updateStatus)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = _TaskService.UpdateStatus(Id, updateStatus);
        return Accepted(updatedTask);
    }
    [HttpPatch("UpdateProgress/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TaskReadDto> UpdateProgress(Guid Id, [FromBody] TaskUpdateProgressDto updateProgress)
    {
        var findTask = _TaskService.FindOne(Id);
        if (findTask == null) return NotFound();
        TaskReadDto? updatedTask = _TaskService.UpdateProgress(Id, updateProgress);
        return Accepted(updatedTask);
    }
}
