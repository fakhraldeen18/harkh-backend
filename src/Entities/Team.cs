namespace Harkh_backend.src.Entities;

public class Team
{

    public Guid Id { get; set; }
    public Guid ProjectId { get; set; } // Foreign key
    public string Name { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    // Navigation properties
    public List <User> Users { get; set; }


}
