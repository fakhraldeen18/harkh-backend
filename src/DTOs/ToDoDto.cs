using System.ComponentModel.DataAnnotations;
using Todo_backend.src.Enums;

namespace Todo_backend.src.DTOs;

public class ToDoReadDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
public class ToDoCreteDto
{
    public Guid UserId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    public Status Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
public class ToDoUpdateDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public Status Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
public class ToDoUpdateStatusDto
{
    [Required]
    public Status Status { get; set; }
}
