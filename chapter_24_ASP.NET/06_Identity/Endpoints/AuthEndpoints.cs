using System.Security.Claims;
using BlogApi.Dtos.Auth;
using BlogApi.Filters;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpionts(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        // POST /auth/register
        group.MapPost("/register", async (RegisterRequestDto req, IAuthService auth) =>
        {
            var (success, errors) = await auth.RegisterAsync(req);
            return success ? Results.Ok() : Results.BadRequest(errors);
        })
        .WithValidation<RegisterRequestDto>()
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        // POST /auth/login
        group.MapPost("/login", async (LoginRequestDto req, IAuthService auth) =>
        {
            var result = await auth.LoginAsync(req);
            return result is not null ? Results.Ok(result) : Results.Unauthorized();
        })
        .WithValidation<LoginRequestDto>()
        .Produces<AuthResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

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