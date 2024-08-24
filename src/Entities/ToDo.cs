using System.ComponentModel.DataAnnotations;

namespace Todo_backend.src.Entities;
public class ToDo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description  { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}
