using Desafio3API.DTOs;
using Desafio3API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Desafio3API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var response = await _authService.Login(loginDto);

        if (response == null)
        {
            return Unauthorized("Email ou senha inválidos.");
        }

        return Ok(response);
    }
}