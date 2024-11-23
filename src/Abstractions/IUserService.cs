using Harkh_backend.src.DTOs;

namespace Harkh_backend.src.Abstractions;

public interface IUserService
{


    public Task<IEnumerable<UserReadDto>> FindAll();
    public Task<UserReadDto?> FindOne(Guid id);
    public Task<string?> Login(UserLogInDto user);
    public Task<UserReadDto?> SignUp(UserCreateDto user);
    public Task<bool>DeleteOne(Guid id);
    public Task<UserReadDto?> UpdateOne(Guid id, UserUpdateDto updatedUser);
    public Task<UserReadDto?> UpdateRole(Guid id, UserUpdateRoleDto updatedUser);

}
