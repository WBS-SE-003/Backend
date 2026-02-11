using BudgetApi.Models;
using BudgetApi.Dtos.Transactions;

namespace BudgetApi.Services.Interfaces;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionResponseDto>> ListAsync(Guid userId);
    Task<TransactionResponseDto?> GetAsync(Guid userId, Guid id);
    Task<TransactionResponseDto> CreateAsync(Guid userId, CreateTransactionDto dto);
    Task<TransactionResponseDto?> UpdateAsync(Guid userId, Guid id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(Guid userId, Guid id);
}
