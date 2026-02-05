using BudgetApi.Dtos.Transactions;
using BudgetApi.Services.Interfaces;

namespace BudgetApi.Api.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions").WithTags("Transactions");

        group.MapGet("/", (ITransactionService svc) => svc.ListAsync());

        group.MapGet("/{id:guid}", (Guid id, ITransactionService svc) => svc.GetAsync(id));

        group.MapPost("/", (CreateTransactionDto dto, ITransactionService svc) =>
            svc.CreateAsync(dto));

        group.MapPut("/{id:guid}", (Guid id, UpdateTransactionDto dto, ITransactionService svc) =>
            svc.UpdateAsync(id, dto));

        group.MapDelete("/{id:guid}", (Guid id, ITransactionService svc) => svc.DeleteAsync(id));
    }
}