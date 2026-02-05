using BudgetApi.Models;
using BudgetApi.Dtos.Transactions;

namespace BudgetApi.Services.Interfaces;

public interface ITransactionService
{
    Task<IReadOnlyList<Transaction>> ListAsync();
    Task<Transaction?> GetAsync(Guid id);
    Task<Transaction> CreateAsync(CreateTransactionDto dto);
    Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(Guid id);
}
