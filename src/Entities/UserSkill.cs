namespace Harkh_backend.src.Entities;

public class UserSkill
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    public Guid SkillId { get; set; } // foreign key

}
