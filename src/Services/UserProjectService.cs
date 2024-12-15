using System.Collections;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class UserProjectService : IUserProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<UserProject> _userProjectRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;


    public UserProjectService(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userProjectRepository = _unitOfWork.UserProjects;
        _userRepository = _unitOfWork.Users;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserProject>> FindAll()
    {
        return await _userProjectRepository.FindAll();
    }

    public async Task<IEnumerable?> GetProjectUsers(Guid projectId)
    {
        var findProject = await _projectRepository.FindOne(projectId);
        if (findProject == null) return null;
        var projects = await _projectRepository.FindAll();
        var users = await _userRepository.FindAll();
        var userProjects = await _userProjectRepository.FindAll();
        var projectUsers = from userProject in userProjects
                           join user in users
                           on userProject.UserId equals user.Id
                           join project in projects
                           on userProject.ProjectId equals project.Id
                           join manger in users
                           on project.UserId equals manger.Id
                           where project.Id == projectId
                           select new
                           {
                               namOfProject = project.Name,
                               managerName = manger.Name,
                               userName = user.Name
                           };
        return projectUsers;

    }

    public async Task<IEnumerable?> GetUserProjects(Guid userId)
    {
        var findUser = await _userRepository.FindOne(userId);
        if (findUser == null) return null;
        var projects = await _projectRepository.FindAll();
        var users = await _userRepository.FindAll();
        var userProjects = await _userProjectRepository.FindAll();
        var readUserProject = from userProject in userProjects
                              join user in users
                              on userProject.UserId equals user.Id
                              join project in projects
                              on userProject.ProjectId equals project.Id
                              where user.Id == userId
                              select new
                              {
                                  userName = user.Name,
                                  namOfProject = project.Name

                              };
        return readUserProject;
    }

    public async Task<UserProject?> CreateOne(UsersProjectsCreateDto newUserProject)
    {
        if (newUserProject == null) return null;
        await _unitOfWork.BeginTransaction();
        try
        {
            var createdUserProject = _mapper.Map<UserProject>(newUserProject);
            await _userProjectRepository.CreateOne(createdUserProject);
            await _unitOfWork.Complete();
            return createdUserProject;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<bool> DeleteOne(Guid id)
    {
        var result = await _userProjectRepository.FindOne(id);
        if (result == null) return false;
        await _unitOfWork.BeginTransaction();
        try
        {
            _userProjectRepository.DeleteOne(result);
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
}
