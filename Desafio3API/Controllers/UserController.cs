using Desafio3API.DTOs;
using Desafio3API.Models;
using Desafio3API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio3API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllUsers();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(user);
    }

    [HttpPost]
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
    {
        var user = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Password = createUserDto.Password
        };

        var createdUser = await _userService.CreateUser(user);

        if (createdUser == null)
        {
            return BadRequest("Já existe um usuário cadastrado com esse email.");
        }

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = createdUser.Id },
            createdUser
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var updatedUser = new User
        {
            Name = updateUserDto.Name,
            Email = updateUserDto.Email,
            Password = updateUserDto.Password
        };

        var user = await _userService.UpdateUser(id, updatedUser);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteUser(id);

        if (!deleted)
        {
            return NotFound("Usuário não encontrado.");
        }

        return NoContent();
    }
}