using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class ExperienceController : CustomController
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceReadDto>>> FindAll()
    {
        var experiences = await _experienceService.FindAll();
        return Ok(experiences);
    }
}
