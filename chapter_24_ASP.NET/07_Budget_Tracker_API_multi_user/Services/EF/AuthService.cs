using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BudgetApi.Dtos.Auth;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BudgetApi.Services.EF;

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
        // 1) find the user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return null;

        // 2) verify the password
        var ok = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!ok) return null;

        // 3) read jwt secret from config, and create signing credentials
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //4) build claims (data we put inside the token)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("displayName", user.Name)
        };

        // 5)) set-up token expiry time
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiryMinutes"]!));

        // 6) Create JWT Token(issuer/Audience + claims + expiry + signature)
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        // 7)) Return token + expiry timestamp to the clien
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

        return new { id = user.Id, displayName = user.Name, email = user.Email };
    }
}