using BudgetApi.Models;

namespace BudgetApi.Dtos.Transactions;

public record TransactionResponseDto(
    Guid id,
    TransactionType Type,
    string Description,
    decimal Amount,
    DateTimeOffset Timestamp,
    DateOnly Date
);