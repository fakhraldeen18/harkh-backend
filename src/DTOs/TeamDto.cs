namespace Harkh_backend.src.DTOs;

public class TeamReadDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; } // Foreign key
    public string Name { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
public class TeamCreateDto
{
    public Guid? ProjectId { get; set; } // Foreign key
    public string Name { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
}
public class TeamUpdateDto
{
    public Guid ProjectId { get; set; } // Foreign key
    public string Name { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
