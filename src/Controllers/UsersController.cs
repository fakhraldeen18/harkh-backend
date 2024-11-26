using Harkh_backend.src.Abstractions;
using Harkh_backend.src.DTOs;
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
    public async Task<ActionResult<IEnumerable<UserReadDto>>> FindAll()
    {
        IEnumerable<UserReadDto> users = await _userService.FindAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadDto>> FindOne(Guid id)
    {
        UserReadDto? findUser = await _userService.FindOne(id);
        if (findUser == null) return NotFound();
        return Ok(findUser);
    }

    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserReadDto>> SignUp([FromBody] UserCreateDto newUser)
    {
        if (newUser == null) return BadRequest();
        UserReadDto? createdUser = await _userService.SignUp(newUser);
        return CreatedAtAction(nameof(SignUp), createdUser);
    }

    [HttpPost("logIn")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string?>> LogIn([FromBody] UserLogInDto user)
    {
        if (user == null) return BadRequest();
        string? token = await _userService.Login(user);
        if (token == null) return BadRequest();
        return Ok(token);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadDto>> UpdateOne(Guid id, [FromBody] UserUpdateDto updateUser)
    {
        UserReadDto? findUser = await _userService.FindOne(id);
        if (findUser == null) return NotFound();
        UserReadDto? updatedUser = await _userService.UpdateOne(id, updateUser);
        return Accepted(updatedUser);
    }
    [HttpPatch("rolePromotion/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadDto>> UpdateRole(Guid id, [FromBody] UserUpdateRoleDto updateUser)
    {
        UserReadDto? findUser = await _userService.FindOne(id);
        if (findUser == null) return NotFound();
        UserReadDto? updatedUser = await _userService.UpdateRole(id, updateUser);
        return Accepted(updatedUser);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOne(Guid id)
    {
        var findUser = await _userService.FindOne(id);
        if (findUser == null) return NotFound();
        await _userService.DeleteOne(id);
        return NoContent();
    }
}