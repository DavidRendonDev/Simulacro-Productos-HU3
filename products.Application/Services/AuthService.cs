using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using products.Domain.Interfaces;
using products.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace products.Application.Services;

public class AuthService
{
    private readonly IUsuariosRepository _usuariosRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuariosRepository usuariosRepository, IConfiguration configuration)
    {
        _usuariosRepository = usuariosRepository;
        _configuration = configuration;
    }
    
    //Registro usuario creado
    public async Task<Usuario?> Register(Usuario usuario)
    {
        var existing = await _usuariosRepository.GetByEmail(usuario.Email);
        if (existing != null) return null;

        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        await _usuariosRepository.Create(usuario);
        return usuario;
    }
    
    //login => genera el token 

    public async Task<string?> Login(string email, string password)
    {
        var usuario = await _usuariosRepository.GetByEmail(email);
        if (usuario ==  null) return null;

        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
        if (!passwordValid) return null;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
            new Claim(ClaimTypes.Role, usuario.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials:  creds
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}