using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;


    public ProjectService(IMapper mapper, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectReadDto?> CreateOne(ProjectCreateDto newProject)
    {
        Project? project = _mapper.Map<Project>(newProject);
        if (project == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            await _projectRepository.CreateOne(project);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<ProjectReadDto>(project);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<bool> DeleteOne(Guid id)
    {
        Project? findProject = await _projectRepository.FindOne(id);
        if (findProject == null) return false;
        await _unitOfWork.BeginTransaction();
        try
        {
            _projectRepository.DeleteOne(findProject);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return true;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return false;
        }
    }

    public async Task<IEnumerable<ProjectReadDto>> FindAll()
    {
        IEnumerable<Project> projects = await _projectRepository.FindAll();
        IEnumerable<ProjectReadDto> readProjects = _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        return readProjects;
    }

    public async Task<ProjectReadDto?> FindOne(Guid id)
    {
        var findProject = await _projectRepository.FindOne(id);
        if (findProject == null) return null;
        return _mapper.Map<ProjectReadDto>(findProject);
    }

    public async Task<ProjectReadDto?> UpdateOne(Guid id, ProjectUpdateDto updatedProject)
    {
        Project? project = await _projectRepository.FindOne(id);
        if (project == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            project.UserId = updatedProject.UserId;
            project.Name = updatedProject.Name;
            project.Progress = updatedProject.Progress;
            project.Description = updatedProject.Description;
            project.StartDate = updatedProject.StartDate;
            project.EndDate = updatedProject.EndDate;
            project.Status = updatedProject.Status;
            project.UpdateAt = updatedProject.UpdateAt;
            _projectRepository.UpdateOne(project);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<ProjectReadDto>(project);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<ProjectReadDto?> UpdateStatus(Guid id, ProjectUpdateStatusDto updatedProject)
    {
        Project? project = await _projectRepository.FindOne(id);
        if (project == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            project.Status = updatedProject.Status;
            project.UpdateAt = updatedProject.UpdateAt;
            _projectRepository.UpdateOne(project);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return _mapper.Map<ProjectReadDto>(project);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }
}
