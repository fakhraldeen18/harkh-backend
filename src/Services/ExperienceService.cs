using AutoMapper;
using Harkh_backend.src.UnitOfWork;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class ExperienceService : IExperienceService
{
    private readonly IBaseRepository<Experience> _experienceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExperienceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _experienceRepository = _unitOfWork.Experiences;
        _mapper = mapper;
    }

    public IEnumerable<ExperienceReadDto> FindAll()
    {
        var experiences = _experienceRepository.FindAll();
        var readExperiences = _mapper.Map<IEnumerable<ExperienceReadDto>>(experiences);
        return readExperiences;
    }
}
