using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Entities;

[Index(nameof(Email), IsUnique = true)] // Index is to Search by email faster. 

public class User
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Role Role { get; set; } = Role.TeamMember;
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Task> Tasks { get; set; } // Navigation order
}