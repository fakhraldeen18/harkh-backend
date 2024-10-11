using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Harkh_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;

public class DocumentRepository : IDocumentRepository
{

    private readonly DbSet<Document> _document;
    private readonly DatabaseContext _databaseContext;

    public DocumentRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _document = databaseContext.Documents;
    }

    public Document CreateOne(Document newDocument)
    {
        _document.Add(newDocument);
        _databaseContext.SaveChanges();
        return newDocument;
    }

    public Document? DeleteOne(Guid id)
    {
        Document? findDocument = FindOne(id);
        if (findDocument == null) return null;
        _document.Remove(findDocument);
        _databaseContext.SaveChanges();
        return findDocument;
    }

    public IEnumerable<Document> FindAll()
    {
        return _document;
    }

    public Document? FindOne(Guid id)
    {
        Document? findDocument = _document.FirstOrDefault(d => d.Id == id);
        return findDocument;
    }

    public Document UpdateOne(Document updatedDocument)
    {
        _document.Update(updatedDocument);
        _databaseContext.SaveChanges();
        return updatedDocument;
    }
}
