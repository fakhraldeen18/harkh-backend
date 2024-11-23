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

    public async Task<SkillReadDto?> CreateOne(SkillCreateDto newSkill)
    {
        if (newSkill == null) return null;
        var skill = _mapper.Map<Skill>(newSkill);
        await _skillRepository.CreateOne(skill);
        await _unitOfWork.Complete();
        return _mapper.Map<SkillReadDto>(skill);
    }

    public async Task<IEnumerable<SkillReadDto>> FindAll()
    {
        var skills = await _skillRepository.FindAll();
        return _mapper.Map<IEnumerable<SkillReadDto>>(skills);
    }

    public async Task<SkillReadDto?> UpdateOne(Guid id, SkillUpdateDto updateSkill)
    {
        var findSkill = await _skillRepository.FindOne(id);
        if (findSkill == null) return null;
        findSkill.Name = updateSkill.Name;
        _skillRepository.UpdateOne(findSkill);
        await _unitOfWork.Complete();
        return _mapper.Map<SkillReadDto>(findSkill);
    }
}
