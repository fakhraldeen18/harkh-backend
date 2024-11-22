using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;

namespace Harkh_backend.src.Entities;
public class Task
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    public Guid? MilestoneId { get; set; } // foreign key
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public float Progress { get; set; }
    [Required]
    public Status Status { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public DateTime UpdateAt { get; set; }
}
