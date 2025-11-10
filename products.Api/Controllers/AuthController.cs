using Microsoft.AspNetCore.Mvc;
using products.Application.Services;
using products.Domain.Models;

namespace products.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly AuthService _authService;
    
    public  AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Usuario usuario)
    {
        var CreateUsuario = await _authService.Register(usuario);
        if (CreateUsuario == null) return BadRequest();
        return Ok(CreateUsuario);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Usuario usuario)
    {
        var token  = await _authService.Login(usuario.Email, usuario.Password);
        if (token == null) return Unauthorized();
        return Ok(token);
    }
    
    public record LoginRequest(string Email, string Password);
}