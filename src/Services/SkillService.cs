using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class SkillService : ISkillService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Skill> _skillRepository;
    private readonly IMapper _mapper;

    public SkillService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _skillRepository = _unitOfWork.Skills;
        _mapper = mapper;
    }

    public SkillReadDto? CreateOne(SkillCreateDto newSkill)
    {
        if (newSkill == null) return null;
        var skill = _mapper.Map<Skill>(newSkill);
        _skillRepository.CreateOne(skill);
        _unitOfWork.Complete();
        return _mapper.Map<SkillReadDto>(skill);
    }

    public IEnumerable<SkillReadDto> FindAll()
    {
        var skills = _skillRepository.FindAll();
        return _mapper.Map<IEnumerable<SkillReadDto>>(skills);
    }

    public SkillReadDto? UpdateOne(Guid id, SkillUpdateDto updateSkill)
    {
        var findSkill = _skillRepository.FindOne(id);
        if (findSkill == null) return null;
        findSkill.Name = updateSkill.Name;
        _skillRepository.UpdateOne(findSkill);
        _unitOfWork.Complete();
        return _mapper.Map<SkillReadDto>(findSkill);
    }
}
