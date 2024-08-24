using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Todo_backend.src.Entities;

[Index(nameof(Email), IsUnique = true)] // Index is to Search by email faster. 

public class User
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Phone { get; set; }

    public List<ToDo> ToDos { get; set; } // Navigation order
}