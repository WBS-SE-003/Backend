using BlogApi.Dtos.Auth;
using BlogApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<User> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<(bool Success, IEnumerable<object> Errors)> RegisterAsync(RegisterRequestDto request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name

        };

        var result = await _userManager.CreateAsync(user, request.Password);
        return (result.Succeeded, result.Errors);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return null;

        var ok = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!ok) return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("displayName", user.Name)
        };

        var expires = DateTime.UtcNow.AddMinutes(
            int.Parse(_config["Jwt:ExpiryMinutes"]!)
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAtUtc = expires
        };
    }

    public async Task<object?> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return null;

        return new
        {
            id = user.Id,
            displayName = user.Name,
            email = user.Email
        };
    }
}