using System.ComponentModel.DataAnnotations;

namespace Harkh_backend.src.DTOs;


public class UserSkillCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid SkillId { get; set; }

}
