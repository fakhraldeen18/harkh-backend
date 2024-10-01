using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;

namespace Harkh_backend.src.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IMapper mapper, IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public ProjectReadDto? CreateOne(ProjectCreateDto newProject)
    {
        Project? project = _mapper.Map<Project>(newProject);
        if (project == null) return null;
        _projectRepository.CreateOne(project);
        return _mapper.Map<ProjectReadDto>(project);
    }

    public bool DeleteOne(Guid id)
    {
        Project? findProject = _projectRepository.FindOne(id);
        if (findProject == null) return false;
        _projectRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<ProjectReadDto> FindAll()
    {
        IEnumerable<Project> projects = _projectRepository.FindAll();
        IEnumerable<ProjectReadDto> ReadProjects = _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        return ReadProjects;
    }

    public ProjectReadDto? FindOne(Guid id)
    {
        var findProject = _projectRepository.FindOne(id);
        if (findProject == null) return null;
        return _mapper.Map<ProjectReadDto>(findProject);
    }

    public ProjectReadDto? UpdateOne(Guid id, ProjectUpdateDto updatedProject)
    {
        Project? project = _projectRepository.FindOne(id);
        if (project == null) return null;
        project.UserId = updatedProject.UserId;
        project.Name = updatedProject.Name;
        project.Progress = updatedProject.Progress;
        project.Description = updatedProject.Description;
        project.StartDate = updatedProject.StartDate;
        project.EndDate = updatedProject.EndDate;
        project.Status = updatedProject.Status;
        project.UpdateAt = updatedProject.UpdateAt;
        _projectRepository.UpdateOne(project);
        return _mapper.Map<ProjectReadDto>(project);
    }

    public ProjectReadDto? UpdateStatus(Guid id, ProjectUpdateStatusDto updatedProject)
    {
        Project? project = _projectRepository.FindOne(id);
        if (project == null) return null;
        project.Status = updatedProject.Status;
        project.UpdateAt = updatedProject.UpdateAt;
        _projectRepository.UpdateOne(project);
        return _mapper.Map<ProjectReadDto>(project);
    }
}
