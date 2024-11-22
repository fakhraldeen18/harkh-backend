using System.ComponentModel.DataAnnotations;

namespace Harkh_backend.src.DTOs;

public class ExperienceReadDto
{

    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    public string Title { get; set; }
    public string Description { get; set; }
    public string EmploymentType { get; set; }
    public string CompanyName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
public class ExperienceCreateDto
{
    public Guid UserId { get; set; } // foreign key
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? EmploymentType { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
public class ExperienceUpdateDto
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? EmploymentType { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
