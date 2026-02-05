using BudgetApi.Infrastructure;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Dtos.Transactions;

namespace BudgetApi.Services.EF;

public sealed class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _db;
    public TransactionService(ApplicationDbContext db) => _db = db;

    public async Task<IReadOnlyList<Transaction>> ListAsync()
        => await _db.Transactions.ToListAsync();

    public async Task<Transaction?> GetAsync(Guid id)
       => await _db.Transactions.FindAsync(id);

    public async Task<Transaction> CreateAsync(CreateTransactionDto dto)
    {
        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = dto.Type,
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Timestamp = DateTimeOffset.UtcNow
        };

        _db.Transactions.Add(tx);
        await _db.SaveChangesAsync();
        return tx;
    }

    public async Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var tx = await _db.Transactions.FindAsync(id);
        if (tx is null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            tx.Description = dto.Description;

        if (dto.Amount.HasValue)
            tx.Amount = dto.Amount.Value;

        await _db.SaveChangesAsync();
        return tx;

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tx = await _db.Transactions.FindAsync(id);
        if (tx is null) return false;

        _db.Transactions.Remove(tx);
        await _db.SaveChangesAsync();
        return true;
    }
}