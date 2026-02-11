using BudgetApi.Dtos.Auth;

namespace BudgetApi.Services.Interfaces;

public interface IAuthService
{
    Task<(bool Success, IEnumerable<object> Errors)> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
    Task<object?> GetCurrentUserAsync(string userId);

}