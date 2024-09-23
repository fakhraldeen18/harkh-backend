using Harkh_backend.src.Enums;

namespace Harkh_backend.src.DTOs;

public class ProjectReadDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

}
public class ProjectCreatDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;

}
public class ProjectUpdateDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;

}
public class ProjectUpdateStatusDto
{
    public ProjectStatus Status { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;

}
