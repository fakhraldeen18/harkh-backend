using Microsoft.AspNetCore.Mvc;
using Todo_backend.src.Abstractions;
using Todo_backend.src.DTOs;
using Todo_backend.src.Entities;

namespace Todo_backend.src.Controllers;

public class ToDosController : CustomController
{
    private readonly IToDoService _toDoService;

    public ToDosController(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ToDoReadDto>> FindAll()
    {
        IEnumerable<ToDoReadDto>? toDos = _toDoService.FindAll();
        return Ok(toDos);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ToDoReadDto> FindOne(Guid Id)
    {
        var findTodo = _toDoService.FindOne(Id);
        if (findTodo == null) return NotFound();
        return Ok(findTodo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ToDoReadDto> CreateOne([FromBody] ToDoCreteDto newToDo)
    {
        if (newToDo == null) return BadRequest();
        ToDoReadDto? createdTodo = _toDoService.CreateOne(newToDo);
        return CreatedAtAction(nameof(CreateOne), createdTodo);
    }

    [HttpDelete("{Id}")]
    public ActionResult DeleteOne(Guid Id)
    {
        var findTodo = _toDoService.FindOne(Id);
        if (findTodo == null) return NotFound();
        _toDoService.DeleteOne(Id);
        return NoContent();
    }

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ToDoReadDto> UpdateOne(Guid Id, [FromBody] ToDoUpdateDto updateToDo)
    {
        var findTodo = _toDoService.FindOne(Id);
        if (findTodo == null) return NotFound();
        ToDoReadDto? updatedTodo = _toDoService.UpdateOne(Id, updateToDo);
        return Accepted(updatedTodo);
    }

    [HttpPatch("UpdateStatus/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ToDoReadDto> UpdateStatus(Guid Id, [FromBody] ToDoUpdateStatusDto updateStatus)
    {
        var findTodo = _toDoService.FindOne(Id);
        if (findTodo == null) return NotFound();
        ToDoReadDto? updatedTodo = _toDoService.UpdateStatus(Id, updateStatus);
        return Accepted(updatedTodo);
    }
}
