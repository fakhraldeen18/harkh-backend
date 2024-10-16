using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;

public class ProjectsController : CustomController
{

    private readonly IProjectService _projectService;
    private readonly IDocumentService _documentService;

    public ProjectsController(IProjectService projectService, IDocumentService documentService)
    {
        _projectService = projectService;
        _documentService = documentService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ProjectReadDto>> FindAll()
    {
        IEnumerable<ProjectReadDto> projects = _projectService.FindAll();
        return Ok(projects);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProjectReadDto> CreateOne([FromBody] ProjectCreateDto newProject)
    {
        if (newProject == null) return BadRequest();
        ProjectReadDto? caretProject = _projectService.CreateOne(newProject);
        return CreatedAtAction(nameof(CreateOne), caretProject);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProjectReadDto> FindOne(Guid id)
    {
        ProjectReadDto? project = _projectService.FindOne(id);
        if (project == null) return NotFound();
        return Ok(project);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(Guid id)
    {
        ProjectReadDto? findProject = _projectService.FindOne(id);
        if (findProject == null) return NotFound();
        _projectService.DeleteOne(id);
        return NoContent();
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProjectReadDto> UpdateOne(Guid id, [FromBody] ProjectUpdateDto updateProject)
    {
        ProjectReadDto? findProject = _projectService.FindOne(id);
        if (findProject == null) return NotFound();
        ProjectReadDto? updatedProject = _projectService.UpdateOne(id, updateProject);
        return Accepted(updatedProject);
    }

    [HttpPatch("UpdateStatus/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProjectReadDto> UpdateStatus(Guid id, [FromBody] ProjectUpdateStatusDto updateProjectStatus)
    {
        var findProject = _projectService.FindOne(id);
        if (findProject == null) return NotFound();
        ProjectReadDto? updatedProject = _projectService.UpdateStatus(id, updateProjectStatus);
        return Accepted(updatedProject);
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
