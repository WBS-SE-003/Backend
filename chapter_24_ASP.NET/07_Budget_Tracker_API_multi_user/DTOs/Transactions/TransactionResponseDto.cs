using BudgetApi.Models;

namespace BudgetApi.Dtos.Transactions;

public record TransactionResponseDto(
    Guid Id,
    DateTime Timestamp,
    TransactionType Type,
    string Description,
    decimal Amount,
    DateOnly Date,
    Guid UserId,
    string UserName
);