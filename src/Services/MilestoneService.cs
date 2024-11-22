using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MilestoneService(IMilestoneRepository milestoneRepository, IProjectRepository projectRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _milestoneRepository = milestoneRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<MilestoneReadDto> CreateOne(MilestoneCreateDto newMilestone)
    {
        Milestone milestone = _mapper.Map<Milestone>(newMilestone);
        await _milestoneRepository.CreateOne(milestone);
        if (milestone?.ProjectId == null) return _mapper.Map<MilestoneReadDto>(milestone);
        await _projectRepository.UpdateProgress(milestone.ProjectId);
        await _unitOfWork.Complete();
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public async Task<bool> DeleteOne(Guid id)
    {
        Milestone? milestone = await _milestoneRepository.FindOne(id);
        if (milestone == null) return false;
        _milestoneRepository.DeleteOne(milestone);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<IEnumerable<MilestoneReadDto>> FindAll()
    {
        IEnumerable<Milestone> milestones = await _milestoneRepository.FindAll();
        return _mapper.Map<IEnumerable<MilestoneReadDto>>(milestones);
    }

    public async Task<MilestoneReadDto?> FindOne(Guid id)
    {
        Milestone? milestone = await _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public async Task<MilestoneReadDto?> UpdateOne(Guid id, MilestoneUpdateDto updatedMilestone)
    {
        Milestone? milestone = await _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        milestone.ProjectId = updatedMilestone.ProjectId;
        milestone.Name = updatedMilestone.Name;
        milestone.Progress = updatedMilestone.Progress;
        milestone.DueDate = updatedMilestone.DueDate;
        _milestoneRepository.UpdateOne(milestone);
        if (milestone?.ProjectId == null) return _mapper.Map<MilestoneReadDto>(milestone);
        await _projectRepository.UpdateProgress(milestone.ProjectId);
        await _unitOfWork.Complete();
        return _mapper.Map<MilestoneReadDto>(milestone);
    }
}
