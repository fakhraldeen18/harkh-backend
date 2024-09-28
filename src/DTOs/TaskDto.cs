using System.ComponentModel.DataAnnotations;
using Harkh_backend.src.Enums;

namespace Harkh_backend.src.DTOs;

public class TaskReadDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid MilestoneId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public string Priority { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public DateTime UpdateAt { get; set; }
}
public class TaskCreteDto
{
    public Guid UserId { get; set; }
    public Guid? MilestoneId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    public Status Status { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public DateTime UpdateAt { get; set; }
}
public class TaskUpdateDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public Status Status { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
public class TaskUpdateStatusDto
{
    [Required]
    public Status Status { get; set; }
}
// public class TaskUpdatePriorityDto
// {
//     [Required]
//     public Priority Priority { get; set; }
// }
