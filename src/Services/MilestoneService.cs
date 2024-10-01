using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public MilestoneService(IMilestoneRepository milestoneRepository, IProjectRepository projectRepository, IMapper mapper)
    {
        _milestoneRepository = milestoneRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public MilestoneReadDto CreateOne(MilestoneCreateDto newMilestone)
    {
        Milestone milestone = _mapper.Map<Milestone>(newMilestone);
        _milestoneRepository.CreateOne(milestone);
        if (milestone?.ProjectId == null) return _mapper.Map<MilestoneReadDto>(milestone);
        _projectRepository.UpdateProgress(milestone.ProjectId);
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public bool DeleteOne(Guid id)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return false;
        _milestoneRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<MilestoneReadDto> FindAll()
    {
        IEnumerable<Milestone> milestones = _milestoneRepository.FindAll();
        return _mapper.Map<IEnumerable<MilestoneReadDto>>(milestones);
    }

    public MilestoneReadDto? FindOne(Guid id)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        return _mapper.Map<MilestoneReadDto>(milestone);
    }

    public MilestoneReadDto? UpdateOne(Guid id, MilestoneUpdateDto updatedMilestone)
    {
        Milestone? milestone = _milestoneRepository.FindOne(id);
        if (milestone == null) return null;
        milestone.ProjectId = updatedMilestone.ProjectId;
        milestone.Name = updatedMilestone.Name;
        milestone.Progress = updatedMilestone.Progress;
        milestone.DueDate = updatedMilestone.DueDate;
        _milestoneRepository.UpdateOne(milestone);
        if (milestone?.ProjectId == null) return _mapper.Map<MilestoneReadDto>(milestone);
        _projectRepository.UpdateProgress(milestone.ProjectId);
        return _mapper.Map<MilestoneReadDto>(milestone);
    }
}
