using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class DocumentService : IDocumentService
{
    private IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Document> _documentRepository;
    private readonly IMapper _mapper;

    public DocumentService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _documentRepository = _unitOfWork.Documents;
        _mapper = mapper;
    }

    public DocumentReadDto? CreateOne(DocumentCreateDto newDocument)
    {
        if (newDocument == null) return null;
        Document createDocument = _mapper.Map<Document>(newDocument);
        _documentRepository.CreateOne(createDocument);
        _unitOfWork.Complete();
        return _mapper.Map<DocumentReadDto>(createDocument);
    }

    public bool DeleteOne(Guid id)
    {
        Document? document = _documentRepository.FindOne(id);
        if (document == null) return false;
        _documentRepository.DeleteOne(id);
        _unitOfWork.Complete();
        return true;
    }

    public IEnumerable<DocumentReadDto> FindAll()
    {
        IEnumerable<Document> documents = _documentRepository.FindAll();
        return _mapper.Map<IEnumerable<DocumentReadDto>>(documents);
    }

    public DocumentReadDto? FindOne(Guid id)
    {
        Document? document = _documentRepository.FindOne(id);
        if (document == null) return null;
        return _mapper.Map<DocumentReadDto>(document);
    }

    public DocumentReadDto? UpdateOne(Guid id, DocumentUpdateDto updatedDocument)
    {
        Document? document = _documentRepository.FindOne(id);
        if (document == null) return null;
        document.FileUrl = updatedDocument.FileUrl;
        _documentRepository.UpdateOne(document);
        _unitOfWork.Complete();
        return _mapper.Map<DocumentReadDto>(document);
    }
}
