using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;

namespace Harkh_backend.src.DTOs;

public class UserReadDto
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UserCreateDto
{
    public Guid TeamId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
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
    public Guid TeamId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
public class UserUpdateRoleDto
{
    [Required]
    public Role Role { get; set; }
    [Required]
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}