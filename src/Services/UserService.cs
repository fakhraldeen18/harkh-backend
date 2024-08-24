using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo_backend.src.Abstractions;
using Todo_backend.src.DTOs;
using Todo_backend.src.Entities;
using Todo_backend.src.Utils;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace Todo_backend.src.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private IConfiguration _config;
    private readonly IMapper _mapper;


    public UserService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
    {
        _userRepository = userRepository;
        _config = config;
        _mapper = mapper;
    }

    public bool DeleteOne(Guid id)
    {
        User? FindUser = _userRepository.FindOne(id);
        if (FindUser == null) return false;
        _userRepository.DeleteOne(id);
        return true;
    }

    public IEnumerable<UserReadDto> FindAll()
    {
        IEnumerable<User> users = _userRepository.FindAll();
        return _mapper.Map<IEnumerable<UserReadDto>>(users);
    }

    public UserReadDto? FindOne(Guid id)
    {
        User? FindUser = _userRepository.FindOne(id);
        if (FindUser == null) return null;
        return _mapper.Map<UserReadDto>(FindUser);
    }

    public string? Login(UserLogInDto user)
    {
        IEnumerable<User>? users = _userRepository.FindAll();
        User? isUser = users.FirstOrDefault(u => u.Email == user.Email);
        if (isUser == null) return null;
        byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);
        bool isCorrect = PasswordUtils.VerifyPassword(user.Password, isUser.Password, pepper);
        if (!isCorrect) return null;

        //Create Token 
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, isUser.Name),
                new Claim(ClaimTypes.Email, isUser.Email),
                new Claim(ClaimTypes.NameIdentifier, isUser.Id.ToString()),
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"]!,
            audience: _config["Jwt:Audience"]!,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
            );
        var tokenSettings = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenSettings;
    }

    public UserReadDto? SignUp(UserCreateDto user)
    {
        byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);
        PasswordUtils.HashPassword(user.Password, out string hashedPassword, pepper);
        user.Password = hashedPassword;
        User mappedUser = _mapper.Map<User>(user);
        User newUser = _userRepository.CreateOne(mappedUser);
        UserReadDto readerUser = _mapper.Map<UserReadDto>(newUser);
        return readerUser;
    }

    public UserReadDto? UpdateOne(Guid id, UserUpdateDto updatedUser)
    {
        User? user = _userRepository.FindOne(id);
        if (user == null) return null;
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Phone = updatedUser.Phone;
        _userRepository.UpdateOne(user);
        return _mapper.Map<UserReadDto>(user);
    }
}
