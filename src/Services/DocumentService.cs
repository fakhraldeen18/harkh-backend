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

    public async Task<DocumentReadDto?> CreateOne(DocumentCreateDto newDocument)
    {
        if (newDocument == null) return null;
        Document createDocument = _mapper.Map<Document>(newDocument);
        await _documentRepository.CreateOne(createDocument);
        await _unitOfWork.Complete();
        return _mapper.Map<DocumentReadDto>(createDocument);
    }

    public async Task<bool> DeleteOne(Guid id)
    {
        Document? document = await _documentRepository.FindOne(id);
        if (document == null) return false;
        _documentRepository.DeleteOne(document);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<IEnumerable<DocumentReadDto>> FindAll()
    {
        IEnumerable<Document> documents = await _documentRepository.FindAll();
        return _mapper.Map<IEnumerable<DocumentReadDto>>(documents);
    }

    public async Task<DocumentReadDto?> FindOne(Guid id)
    {
        Document? document = await _documentRepository.FindOne(id);
        if (document == null) return null;
        return _mapper.Map<DocumentReadDto>(document);
    }

    public async Task<DocumentReadDto?> UpdateOne(Guid id, DocumentUpdateDto updatedDocument)
    {
        Document? document = await _documentRepository.FindOne(id);
        if (document == null) return null;
        document.FileUrl = updatedDocument.FileUrl;
        _documentRepository.UpdateOne(document);
        await _unitOfWork.Complete();
        return _mapper.Map<DocumentReadDto>(document);
    }
}
