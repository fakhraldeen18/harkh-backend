using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;

namespace Harkh_backend.src.DTOs;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string Position { get; set; }
    public string? ProfileImage { get; set; }

    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UserCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Position { get; set; }
    public string? ProfileImage { get; set; }

    public DateTime BirthDate { get; set; }
    [Required]
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class UserLogInDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

public class UserUpdateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    public string? ProfileImage { get; set; }

}
public class UserUpdateRoleDto
{
    [Required]
    public Role Role { get; set; }
}