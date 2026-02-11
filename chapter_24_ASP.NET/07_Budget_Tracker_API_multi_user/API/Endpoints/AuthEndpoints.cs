using System.Security.Claims;
using BudgetApi.Dtos.Auth;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace BudgetApi.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        // POST /auth/register
        group.MapPost("/register", async (RegisterRequestDto req, IAuthService auth) =>
        {
            var (success, errors) = await auth.RegisterAsync(req);
            return success ? Results.Ok() : Results.BadRequest(errors);

        });

        // POST /auth/login
        group.MapPost("/login", async (LoginRequestDto req, IAuthService auth) =>
        {
            var result = await auth.LoginAsync(req);
            return result is not null ? Results.Ok(result) : Results.Unauthorized();

        });

        // GET /auth/me
        group.MapGet("/me", [Authorize] async (ClaimsPrincipal user, IAuthService auth) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            var me = await auth.GetCurrentUserAsync(userId);
            return me is not null ? Results.Ok(me) : Results.NotFound();
        });
    }



}