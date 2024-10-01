namespace Harkh_backend.src.DTOs;

public class MilestoneReadDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public float Progress { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
public class MilestoneCreateDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public float Progress { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
}
public class MilestoneUpdateDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public float Progress { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
public class MilestoneUpdateProgressDto
{
    public float Progress { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
// public class MilestoneUpdateStatusDto
// {
//     public Status Status { get; set; }
//     public DateTime UpdateAt { get; set; } = DateTime.Now;
// }
