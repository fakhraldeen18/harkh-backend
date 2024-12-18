using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;
using Harkh_backend.src.Entities;
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
    public DateTime BirthDate { get; set; }
    [Required]
    public string Position { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    // Navigation properties
    public List<Project> Project { get; set; }
    public List<Task> Tasks { get; set; }
    public List<Document> Documents { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<UserSkill> UserSkills { get; set; }
    public List<UserProject> UserProjects { get; set; }
}