namespace Harkh_backend.src.Entities;
public class Milestone
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; } // Foreign key
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

}
