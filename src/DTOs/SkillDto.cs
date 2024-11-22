namespace Harkh_backend.src.DTOs;

public class SkillReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class SkillCreateDto
{
    public string Name { get; set; }
}
public class SkillUpdateDto
{
    public string Name { get; set; }
}
