namespace Harkh_backend.src.Entities;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public List<UserSkill> UserSkills { get; set; }
}
