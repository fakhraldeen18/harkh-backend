using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Abstractions;

public interface IDocumentRepository
{
    public IEnumerable<Document> FindAll();
    public Document? FindOne(Guid id);
    public Document CreateOne(Document newDocument);
    public Document? DeleteOne(Guid id);
    public Document UpdateOne(Document updatedDocument);

}
