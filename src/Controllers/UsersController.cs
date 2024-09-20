using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harkh_backend.src.Controllers;
public class UsersController : CustomController
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<UserReadDto>> FindAll()
    {
        IEnumerable<UserReadDto> Users = _userService.FindAll();
        return Ok(Users);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserReadDto> FindOne(Guid Id)
    {
        UserReadDto? FindUser = _userService.FindOne(Id);
        if (FindUser == null) return NotFound();
        return Ok(FindUser);
    }

    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserReadDto> SignUp([FromBody] UserCreateDto NewUser)
    {
        if (NewUser == null) return BadRequest();
        UserReadDto? CreatedUser = _userService.SignUp(NewUser);
        return CreatedAtAction(nameof(SignUp), CreatedUser);
    }

    [HttpPost("logIn")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string?> LogIn([FromBody] UserLogInDto user)
    {
        if (user == null) return BadRequest();
        string? token = _userService.Login(user);
        if (token == null) return BadRequest();
        return Ok(token);
    }

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserReadDto> UpdateOne(Guid Id, [FromBody] UserUpdateDto UpdateUser)
    {
        UserReadDto? findUser = _userService.FindOne(Id);
        if (findUser == null) return NotFound();
        UserReadDto? UpdatedUser = _userService.UpdateOne(Id, UpdateUser);
        return Accepted(UpdatedUser);
    }
    [HttpPatch("UpdateRole/{Id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserReadDto> UpdateRole(Guid Id, [FromBody] UserUpdateRoleDto UpdateUser)
    {
        UserReadDto? findUser = _userService.FindOne(Id);
        if (findUser == null) return NotFound();
        UserReadDto? UpdatedUser = _userService.UpdateRole(Id, UpdateUser);
        return Accepted(UpdatedUser);
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(Guid Id)
    {
        var FindUser = _userService.FindOne(Id);
        if (FindUser == null) return NotFound();
        _userService.DeleteOne(Id);
        return NoContent();
    }
}