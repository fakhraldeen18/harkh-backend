using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IDocumentService
{

    public Task<IEnumerable<DocumentReadDto>> FindAll();
    public Task<DocumentReadDto?> FindOne(Guid id);
    public Task<DocumentReadDto?> CreateOne(DocumentCreateDto newDocument);
    public Task<DocumentReadDto?> UpdateOne(Guid id, DocumentUpdateDto updatedDocument);
    public Task<bool> DeleteOne(Guid id);

}
