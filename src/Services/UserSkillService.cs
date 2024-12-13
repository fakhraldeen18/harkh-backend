using System.Collections;
using AutoMapper;
using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Harkh_backend.src.Entities;
using Harkh_backend.src.UnitOfWork;

namespace Harkh_backend.src.Services;

public class UserSkillService : IUserSkillService
{
    private readonly IBaseRepository<UserSkill> _userSkillRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Skill> _skillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserSkillService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userSkillRepository = _unitOfWork.UserSkills;
        _userRepository = _unitOfWork.Users;
        _skillRepository = _unitOfWork.Skills;
        _mapper = mapper;
    }

    public async Task<IEnumerable> FindAll()
    {
        return await _userSkillRepository.FindAll();
    }

    public async Task<UserSkill?> CreateOne(UserSkillCreateDto newUserSkill)
    {
        if (newUserSkill == null) return null;

        try
        {
            var userSkill = _mapper.Map<UserSkill>(newUserSkill);
            await _unitOfWork.BeginTransaction();
            await _userSkillRepository.CreateOne(userSkill);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return userSkill;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            return null;
        }
    }

    public async Task<IEnumerable?> GetUserSkills(Guid userId)
    {
        var findUser = await _userRepository.FindOne(userId);
        if (findUser == null) return null;
        var userSkills = await _userSkillRepository.FindAll();
        var users = await _userRepository.FindAll();
        var skills = await _skillRepository.FindAll();
        var joinUserSkill = from userSkill in userSkills
                            join user in users
                            on userSkill.UserId equals user.Id
                            join skill in skills
                            on userSkill.SkillId equals skill.Id
                            where user.Id == userId
                            select new
                            {
                                name = user.Name,
                                skillName = skill.Name
                            };
        return joinUserSkill;

    }


    public async Task<bool> DeleteOne(Guid id)
    {
        var isUserSkill = await _userSkillRepository.FindOne(id);
        if (isUserSkill == null) return false;
        try
        {
            await _unitOfWork.BeginTransaction();
            _userSkillRepository.DeleteOne(isUserSkill);
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
