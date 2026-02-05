using BudgetApi.Infrastructure;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Dtos.Reports;

namespace BudgetApi.Services.EF;

public sealed class ReportService : IReportService
{
    private readonly ApplicationDbContext _db;
    public ReportService(ApplicationDbContext db) => _db = db;

    public async Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end)
    {
        // collect all transactions that fall within the given date range
        var txs = await _db.Transactions
            .Where(t => t.Date >= start && t.Date <= end)
            .ToListAsync();

        // calculate total income&expense by summing amounts of transactions
        var income = txs.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var expense = txs.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        return new SummaryReportResponseDto(start, end, income, expense, income - expense);
    }
}
