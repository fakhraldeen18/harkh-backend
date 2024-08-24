using Microsoft.AspNetCore.Mvc;

namespace Todo_backend.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class CustomController : ControllerBase
{

}
