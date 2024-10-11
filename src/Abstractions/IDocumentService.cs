using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IDocumentService
{

    public IEnumerable<DocumentReadDto> FindAll();
    public DocumentReadDto? FindOne(Guid id);
    public DocumentReadDto? CreateOne(DocumentCreateDto newDocument);
    public DocumentReadDto? UpdateOne(Guid id, DocumentUpdateDto updatedDocument);
    public bool DeleteOne(Guid id);

}
