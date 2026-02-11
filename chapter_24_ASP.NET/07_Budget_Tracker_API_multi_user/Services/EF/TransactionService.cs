using BudgetApi.Dtos.Transactions;
using BudgetApi.Infrastructure;
using BudgetApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApi.Services.EF;

public sealed class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _db;
    public TransactionService(ApplicationDbContext db) => _db = db;

    public async Task<IReadOnlyList<TransactionResponseDto>> ListAsync(Guid userId)
    => await _db.Transactions
        .Where(t => t.UserId == userId)
        .OrderByDescending(t => t.Timestamp)
        .Select(t => new TransactionResponseDto(
            t.Id,
            t.Timestamp,
            t.Type,
            t.Description,
            t.Amount,
            t.Date,
            t.UserId,
              _db.Users
            .Where(u => u.Id == t.UserId)
            .Select(u => u.Name)
            .FirstOrDefault() ?? ""

        ))
        .ToListAsync();


    public async Task<TransactionResponseDto?> GetAsync(Guid userId, Guid id)
        => await _db.Transactions
            .Where(t => t.UserId == userId && t.Id == id)
            .Select(t => new TransactionResponseDto(
                t.Id,
                t.Timestamp,
                t.Type,
                t.Description,
                t.Amount,
                t.Date,
                t.UserId,
                _db.Users
                .Where(u => u.Id == t.UserId)
                .Select(u => u.Name)
                .FirstOrDefault() ?? ""

            )).FirstOrDefaultAsync();

    public async Task<TransactionResponseDto> CreateAsync(Guid userId, CreateTransactionDto dto)
    {
        var tx = new Models.Transaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = dto.Type,
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Timestamp = DateTime.UtcNow

        };

        _db.Transactions.Add(tx);
        await _db.SaveChangesAsync();

        var userName = await _db.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Name)
            .FirstAsync();

        return new TransactionResponseDto(
            tx.Id,
            tx.Timestamp,
            tx.Type,
            tx.Description,
            tx.Amount,
            tx.Date,
            tx.UserId,
            userName
            );
    }

    public async Task<TransactionResponseDto?> UpdateAsync(Guid userId, Guid id, UpdateTransactionDto dto)
    {
        var tx = await _db.Transactions
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (tx is null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            tx.Description = dto.Description;

        if (dto.Amount.HasValue)
            tx.Amount = dto.Amount.Value;

        await _db.SaveChangesAsync();

        var userName = await _db.Users
         .Where(u => u.Id == userId)
         .Select(u => u.Name)
         .FirstAsync();

        return new TransactionResponseDto(
           tx.Id,
           tx.Timestamp,
           tx.Type,
           tx.Description,
           tx.Amount,
           tx.Date,
           tx.UserId,
           userName
           );
    }

    public async Task<bool> DeleteAsync(Guid userId, Guid id)
    {
        var tx = await _db.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (tx is null) return false;

        _db.Transactions.Remove(tx);
        await _db.SaveChangesAsync();
        return true;
    }
}