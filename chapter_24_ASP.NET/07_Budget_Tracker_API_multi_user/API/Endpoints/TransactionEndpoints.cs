using System.Security.Claims;
using BudgetApi.Dtos.Transactions;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BudgetApi.Api.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions")
        .WithTags("Transactions")
        .RequireAuthorization(); // protects all endpoints in this group

        // GET /api/transacitons
        group.MapGet("/", async (ClaimsPrincipal user, ITransactionService svc) =>
        {
            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var list = await svc.ListAsync(userId.Value);
            return Results.Ok(list);
        });

        // GET /api/transactions/{id}
        group.MapGet("{id:guid}", async (Guid id, ClaimsPrincipal user, ITransactionService svc) =>
        {
            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var tx = await svc.GetAsync(userId.Value, id);
            return tx is null ? Results.NotFound() : Results.Ok(tx);
        });

        // POST /api/transactions
        group.MapPost("/", async (CreateTransactionDto dto, ClaimsPrincipal user, ITransactionService svc) =>
        {
            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var created = await svc.CreateAsync(userId.Value, dto);
            return Results.Created($"/api/transactions/{created.Id}", created);

        });

        // PUT /api/transactions/{id}
        group.MapPut("/{id:guid}", async (Guid id, UpdateTransactionDto dto, ClaimsPrincipal user, ITransactionService svc) =>
        {

            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var updated = await svc.UpdateAsync(userId.Value, id, dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);

        });

        group.MapDelete("/{id:guid}", async (Guid id, ClaimsPrincipal user, ITransactionService svc) =>
        {
            var userId = GetUserId(user);
            if (userId is null) return Results.Unauthorized();

            var deleted = await svc.DeleteAsync(userId.Value, id);
            return deleted ? Results.NoContent() : Results.NotFound();

        });
    }

    // HELPER to read user from JWT
    private static Guid? GetUserId(ClaimsPrincipal user)
    {
        var raw = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(raw, out var id) ? id : null;
    }
}