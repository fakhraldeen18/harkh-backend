using System.ComponentModel.DataAnnotations;

namespace Harkh_backend.src.Entities;

public class Experience
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string EmploymentType { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }

}
