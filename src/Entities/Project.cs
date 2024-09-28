using Harkh_backend.src.Enums;

namespace Harkh_backend.src.Entities;

public class Project
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public List<Team> Teams { get; set; }
    public List<Milestone> Milestones { get; set; }
}
