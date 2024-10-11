using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class DocumentController : CustomController
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DocumentReadDto>> FindAll()
    {
        var documents = _documentService.FindAll();
        return Ok(documents);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DocumentReadDto> FindOne(Guid id)
    {
        var findDocument = _documentService.FindOne(id);
        if (findDocument == null) return NotFound();
        return Ok(findDocument);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DocumentReadDto> CreateOne([FromBody] DocumentCreateDto newCreateDocument)
    {
        if (newCreateDocument == null) return BadRequest();
        DocumentReadDto? createdDocument = _documentService.CreateOne(newCreateDocument);
        return CreatedAtAction(nameof(CreateOne), createdDocument);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(Guid id)
    {
        var findDocument = _documentService.FindOne(id);
        if (findDocument == null) return NotFound();
        _documentService.DeleteOne(id);
        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DocumentReadDto> UpdateOne(Guid id, DocumentUpdateDto updateDocument)
    {
        var document = _documentService.FindOne(id);
        if (document == null) return NotFound();
        var updatedDocument = _documentService.UpdateOne(id, updateDocument);
        return Accepted(updatedDocument);
    }
}
